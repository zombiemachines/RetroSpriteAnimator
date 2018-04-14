using System.Collections.Generic;
using UnityEngine;

public class RetroSpriteAnimator : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
    private Sprite[] _sprites;
    private int _frameIndex = 0;
    private int _animationFrames = 0;
    private float _deltaTime = 0;
    private List<RetroSpriteAnimation> _animList;
    private RetroSpriteAnimation _currentAnimation;
    private string _previousAnimName = "";

    public void Awake()
    {
        _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        _animList = new List<RetroSpriteAnimation>();
    }

    public void CreateSprite(string ResourceLocation, string SortingLayer = "Default") {
        _sprites = Resources.LoadAll<Sprite>(ResourceLocation);
        _spriteRenderer.sortingLayerName = SortingLayer;
    }

    public void Update () {
        //Reset the frame to 0 if the current animation is different
        if (_previousAnimName != _currentAnimation.name)
        {
            _frameIndex = 0;
            _previousAnimName = _currentAnimation.name;
        }

        //Account for current frame considering variable frame-rate
        _deltaTime += Time.deltaTime;

        if ( _currentAnimation != null )
        {
            while (_deltaTime >= _currentAnimation.frameRate )
            {
                _deltaTime -= _currentAnimation.frameRate;
                _frameIndex++;

                if (_frameIndex >= _animationFrames)
                {
                    if (_currentAnimation.looped) _frameIndex = 0;
                    else { _frameIndex = _animationFrames-1; }
                }
            }

            //Animate sprite with selected frame
            //If you get Array index out of range error, you probably defined an animation referencing a non-existent sprite
            _spriteRenderer.sprite = _sprites[_currentAnimation.frames[_frameIndex]];
        } else
        {
            Debug.Log("Animation doesn't exist: "+ _currentAnimation.name);
        }
    }

    public void AddAnimation(string Name, int[] Frames, float FrameRate = 30, bool Looped = true, bool FlipX = false, bool FlipY = false)
    {
        RetroSpriteAnimation newAnimation = new RetroSpriteAnimation() ;
        newAnimation.name = Name;
        newAnimation.frames = Frames;
        newAnimation.frameRate = 1/FrameRate;
        newAnimation.looped = Looped;
        newAnimation.flipX = FlipX;
        newAnimation.flipY= FlipY;
        _animList.Add(newAnimation);
    }

    public void PlayAnimation(string Name)
    {
        //code to actually switch your animation state to the animation name specified that's stored in animList
        _currentAnimation = _animList.Find(item => item.name.Contains(Name));

        if (_currentAnimation != null) {
            //Store the number of frames in the animation
            _animationFrames = _currentAnimation.frames.Length;
            //Decide whether to flip the gfx or not
            if (_currentAnimation.flipX == true) _spriteRenderer.flipX = true;
            else _spriteRenderer.flipX = false;
            if (_currentAnimation.flipY == true) _spriteRenderer.flipY = true;
            else _spriteRenderer.flipY = false;
        }
    }

    private void AnimationEndCallback()
    {
        //fixme functionality forthcoming coming, something like SendMessageUpwards("AnimCallback", currentAnimName);?
    }

    public class RetroSpriteAnimation
    {
        public string name;
        public int[] frames;
        public float frameRate;
        public bool looped;
        public bool flipX;
        public bool flipY;
    }
}
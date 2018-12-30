using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RetroSpriteAnimator : MonoBehaviour {

    public Sprite sprite;

    public string SortingLayer = "Default";
    public int SortOrder = 1;

    [SerializeField, HideInInspector]
    private Sprite[] _sprites;

    [SerializeField, HideInInspector]
    private SpriteRenderer _spriteRenderer;

    private int _frameIndex = 0;
    private int _animationFrames = 0;
    private float _deltaTime = 0;
    private List<RetroSpriteAnimation> _animList =  new List<RetroSpriteAnimation>();
    private RetroSpriteAnimation _currentAnimation;
    private string _previousAnimName = "";

    //Pushing this code back to OnValidate so this stuff doesn't have to execute at runtime
    private void OnValidate()
    {
        //We don't want this stuff to execute at runtime
        if (Application.isPlaying ) return;

        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        else if (gameObject.GetComponentInChildren<SpriteRenderer>() != null)
        {
            _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        }
        else
        {
            _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        if ( sprite != null )
        {
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.sortingLayerName = SortingLayer;
            _spriteRenderer.sortingOrder = SortOrder;

            Object[] data = AssetDatabase.LoadAllAssetRepresentationsAtPath( AssetDatabase.GetAssetPath(sprite) );
            _sprites = new Sprite[data.Length];
            for (int i = 0; i < data.Length; i++)   
            {
                _sprites[i] = (Sprite)data[i];
            }

            Debug.Log("Set sprite " + AssetDatabase.GetAssetPath(sprite) + " and created an array of sprites of length " + _sprites.Length );
        }
    }

    //Optional methods to create or set sprites in code, if you don't want to use the editor
    public void CreateSprite(string ResourceLocation, string SortingLayer = "Default", int SortOrder = 1 ) {
        _sprites = Resources.LoadAll<Sprite>(ResourceLocation);
        _spriteRenderer.sortingLayerName = SortingLayer;
        _spriteRenderer.sortingOrder = SortOrder;
    }

    public void SetSprite(Sprite[] Resource, string SortingLayer = "Default", int SortOrder = 1)
    {
        _sprites = Resource;
        _spriteRenderer.sortingLayerName = SortingLayer;
        _spriteRenderer.sortingOrder = SortOrder;
    }

    public void Update () {


        //Account for current frame considering variable frame-rate
        _deltaTime += Time.deltaTime;

        if ( _currentAnimation != null )
        {
            //Reset the frame to 0 if the current animation is different
            if (_previousAnimName != _currentAnimation.name)
            {
                _frameIndex = 0;
                _previousAnimName = _currentAnimation.name;
            }

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
            Debug.LogWarning("Animation doesn't exist: ");
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

    public bool IsPlayingAnimation(string Name)
    {
        if (_currentAnimation.name == Name) return true;
        else return false;
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

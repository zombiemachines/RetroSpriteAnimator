# RetroSpriteAnimator
Unity component for easily defining and playing 2D sprite animations in script


<p><strong>Part 2: An Alternative Workflow for Creating and Playing Animations</strong></p>

<figure data-orig-height="246" data-orig-width="638"><img alt="image" data-orig-height="246" data-orig-width="638" src="https://78.media.tumblr.com/ab3cf4841f14680540de6037f1ad1f4a/tumblr_inline_p77591WVDG1sph69g_540.png" /></figure>

<p>Placing sprites frame by frame in an animation clip in Unity is tedious, finicky, and brittle. And then after you make an animation clip you still have to arrange them in mecanim, which auto-generates transition animations (for blended animations so useful for 3D), which you then have to manually remove. If it sounds only kind of annoying, I promise you I undersold it. I remember the good old days of&nbsp;<a href="https://haxeflixel.com/">Flixel</a>&nbsp;where you could play an animation from a sprite sheet in just three lines of code</p>

<figure data-orig-height="70" data-orig-width="474"><img alt="image" data-orig-height="70" data-orig-width="474" src="https://78.media.tumblr.com/5c36e1549e033c48744e96650fb2bf80/tumblr_inline_p7758s9cnq1sph69g_540.png" /></figure>

<p>Enter&nbsp;<a href="https://github.com/dithyrambs/RetroSpriteAnimator">RetroSpriteAnimator</a>! A neat little component I cooked up that replicates Flixel&#39;s simplicity. And in truth, if you&#39;re going to make a tile-based 2D platformer in Unity, you&#39;re probably going to be using your tile editor as your game editor for the majority of development. You might as well go whole hog and just use Unity as a glorified file manager, and bypass all that mecanim nonsense. (Later in this series I&#39;ll share my code for a component friendly class based state machine that will completely replace mecanim). But anyway, you can start by grabbing&nbsp;<a href="https://github.com/dithyrambs/RetroSpriteAnimator">RetroSpriteAnimator from GitHub</a>.</p>

<p>So how to get something as simple as the above Flixel snippet? First,&nbsp;<a href="https://docs.unity3d.com/ScriptReference/GameObject.AddComponent.html">add the RetroSpriteAnimator component</a>&nbsp;to the same game object you want to animate. Then, just call the CreateSprite method.</p>

<figure data-orig-height="35" data-orig-width="519"><img alt="image" data-orig-height="35" data-orig-width="519" src="https://78.media.tumblr.com/7fcfa12fa473390d393b39d44e0d6184/tumblr_inline_p775duRvLd1sph69g_540.png" /></figure>

<p>The first parameter is just the asset path to the sprite-sheet (remember, in order to load assets via code, Unity requires that all assets are placed within the /Assets/Resources directory, so the full asset path in this examples is &quot;.../Assets/Resources/Sprites/16/frog.png&quot;). The optional second parameter is Unity layer you want the asset to be associated with.&nbsp;</p>

<p>So now Unity has a reference to this sprite sheet:</p>

<figure data-orig-height="107" data-orig-width="276"><img alt="image" data-orig-height="107" data-orig-width="276" src="https://78.media.tumblr.com/daf5daa3b6585139ce0d0ac4cf21ded3/tumblr_inline_p775f80M0S1sph69g_540.png" /></figure>

<p>Next we need to define an animation to play. We can do this using the AddAnimation method.</p>

<figure data-orig-height="38" data-orig-width="891"><img alt="image" data-orig-height="38" data-orig-width="891" src="https://78.media.tumblr.com/8e963f2dd48365b02046f3718b395f99/tumblr_inline_p775fmI6WW1sph69g_540.png" /></figure>

<p>In the first parameter, name the animation something. Probably something descriptive and unique. The second parameter defines which animations to use. So with an array of 0, 1, 1, 2, I&#39;m having the game play the first frame, the second frame twice, and then the last frame once (at a frame rate of 7 frames per second, which is what I set agentFrameRate to by the way).</p>

<figure data-orig-height="107" data-orig-width="276"><img alt="image" data-orig-height="107" data-orig-width="276" src="https://78.media.tumblr.com/eb39471ce6086ee99d5789f60cd21aa9/tumblr_inline_p775goYL0W1sph69g_540.png" /></figure>

<p>The last two parameters choose whether the animation loops (we&#39;re making a walking animation, so yes), and if the animation should be flipped horizontally (in frog.png the frog is facing right, but we&#39;re making an animation that&#39;s walking left, so that&#39;s a yes). Now we just play the animation.</p>

<figure data-orig-height="43" data-orig-width="423"><img alt="image" data-orig-height="43" data-orig-width="423" src="https://78.media.tumblr.com/43f6b10b0ebad86ec1180eb4523b94f0/tumblr_inline_p775hlLDrU1sph69g_540.png" /></figure>

<p>With any luck we&#39;ll end up with something like this:</p>

<figure data-orig-height="371" data-orig-width="496"><img alt="image" data-orig-height="371" data-orig-width="496" src="https://78.media.tumblr.com/1889d263edc4b4ad0f8541cc29174f98/tumblr_inline_p775hwP9PF1sph69g_540.gif" /></figure>

<p>Again, feel free to grab&nbsp;<a href="https://github.com/dithyrambs/RetroSpriteAnimator">RetroSpriteAnimator on GitHub</a>, and let me know what you think!</p>

<p>Shoutout to Adam Saltsman (<a href="https://twitter.com/ADAMATOMIC">@ADAMATOMIC</a>), whose open-source Flixel more than inspired RetroSpriteAnimator, and to Luis Zuno (<a href="https://twitter.com/ansimuz">@ansimuz</a>) for his adorably rad Creative Commons art.</p>

Original Post:
http://gamasutra.com/blogs/AlexBelzer/20180416/316583/Working_with_Pixel_Art_Sprites_in_Unity_Animations.php

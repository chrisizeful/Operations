using Godot;

namespace Operations;

/// <summary>
/// Plays the <see cref="Animation"/> from an <see cref="AnimationPlayer"/>. Succeeds
/// when the animation finishes playing. Note that if the animation is set to loop the
/// operation will succeed after the first full playing.
/// </summary>
[Operation("NodeAnimation")]
public class NAnimationOperation : TimeOperation
{

    /// <summary>
    /// The name of the animation to play.
    /// </summary>
    public StringName Animation;
    public double CustomBlend = -1;
    public float CustomSpeed = 1;
    public bool FromEnd;

    public override void Start()
    {
        base.Start();
        AnimationPlayer anim = (AnimationPlayer) Target;
        anim.Play(Animation, CustomBlend, CustomSpeed, FromEnd);
        Duration = anim.CurrentAnimationLength;
    }
}

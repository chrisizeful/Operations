using Godot;

namespace Operations;

/// <summary>
/// Interpolates on a Callable (method) over time from <see cref="From"/> to <see cref="To"/>.
/// </summary>
public class NMethodOperation : Operation
{
    
    /// <summary>
    /// The method to interpolate.
    /// </summary>
    public Callable Method;
    /// <summary>
    /// The starting value.
    /// </summary>
    public Variant From;
    /// <summary>
    /// The ending value.
    /// </summary>
    public Variant To;
    /// <summary>
    /// How long it will take to get from <see cref="From"/> to <see cref="To"/>.
    /// </summary>
    public double Duration;

    public Tween.TransitionType TransType = Tween.TransitionType.Linear;
    public Tween.EaseType EaseType = Tween.EaseType.InOut;

    private Tween _tween;

    public override void Start()
    {
        base.Start();
        _tween = Node.CreateTween();
        _tween.TweenMethod(Method, From, To, Duration).SetTrans(TransType).SetEase(EaseType);
        _tween.BindNode(Node);
        _tween.Finished += Success;
    }

    public override void Restart()
    {
        base.Restart();
        if (GodotObject.IsInstanceValid(_tween))
            _tween.CancelFree();
    }
}
using Godot;

namespace Operations;

/// <summary>
/// Interpolates on a property over time from the current value of the property to that value
/// plus <see cref="Delta"/>.
/// </summary>
[Operation("NodeProperty")]
public class NPropertyOperation : TimeOperation
{

    /// <summary>
    /// The name of the property in the target node to interpolate.
    /// </summary>
    public StringName Property;
    /// <summary>
    /// The value to add.
    /// </summary>
    public Variant Delta;
    
    public Tween.TransitionType TransType = Tween.TransitionType.Linear;
    public Tween.EaseType EaseType = Tween.EaseType.InOut;

    protected Variant _start;

    public override void Start()
    {
        base.Start();
        _start = Node.Get(Property);
    }

    public override Status Act(double delta)
    {
        Status status = base.Act(delta);
        Variant value = Tween.InterpolateValue(_start, Delta, Percent, 1, TransType, EaseType);
        Node.Set(Property, value);
        return status;
    }
}

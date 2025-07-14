using Godot;

namespace Operations;

/// <summary>
/// Interpolates a property on the target node over time.
/// </summary>
public abstract class NRelativeOperation : TimeOperation
{

    /// <summary>
    /// The value to interpolate to.
    /// </summary>
    public Variant Value;
    /// <summary>
    /// The name of the property that will be set to <see cref="Value"/>.
    /// </summary>
    public StringName Property;
    /// <summary>
    /// Whether <see cref="Value"/> is relative to the current value or not. For example,
    /// if Value is Vector2(200, 200) and a Node is positioned at (100, 100):
    /// Relative: ending position will be (300, 300)
    /// Not relative: ending position will be (200, 200)
    /// </summary>
    public bool Relative = true;
    /// <summary>
    /// Whether operations will take place in global or local space.
    /// </summary>
    public bool Global;

    public Tween.TransitionType TransType = Tween.TransitionType.Linear;
    public Tween.EaseType EaseType = Tween.EaseType.InOut;

    protected Variant start, goal;

    public override void Start()
    {
        start = Node.Get(Property);
        goal = DeltaValue();
    }

    protected abstract Variant DeltaValue();

    public override Status Act(double delta)
    {
        Status status = base.Act(delta);
        Node.Set(Property, Interpolate());
        return status;
    }

    protected virtual Variant Interpolate()
    {
        return Tween.InterpolateValue(start, goal, Percent, 1, TransType, EaseType);
    }
}

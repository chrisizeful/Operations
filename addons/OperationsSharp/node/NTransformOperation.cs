using Godot;

namespace Operations;

/// <summary>
/// Interpolates the transform of the target <see cref="Node2D"/>.
/// </summary>
[Operation("NodeTransform2D")]
public class NTransform2DOperation : NRelativeOperation
{

    /// <summary>
    /// The target transform.
    /// </summary>
    public Transform2D Transform
    {
        get => Value.AsTransform2D();
        set => Value = value;
    }

    public override void Start()
    {
        Property = Global ? Node2D.PropertyName.GlobalTransform : Node2D.PropertyName.Transform;
        base.Start();
    }

    protected override Variant DeltaValue()
    {
        Transform2D transform = start.AsTransform2D();
        return Relative ? transform * Transform : Transform;
    }

    protected override Variant Interpolate()
    {
        Transform2D startTransform = start.AsTransform2D();
        Transform2D goalTransform = goal.AsTransform2D();
        return startTransform.InterpolateWith(goalTransform, (float) Percent);
    }
}

/// <summary>
/// Interpolates the transform of the target <see cref="Node3D"/>.
/// </summary>
[Operation("NodeTransform3D")]
public class NTransform3DOperation : NRelativeOperation
{

    /// <summary>
    /// The target transform.
    /// </summary>
    public Transform3D Transform
    {
        get => Value.AsTransform3D();
        set => Value = value;
    }

    public override void Start()
    {
        Property = Global ? Node3D.PropertyName.GlobalTransform : Node3D.PropertyName.Transform;
        base.Start();
    }

    protected override Variant DeltaValue()
    {
        Transform3D delta = start.AsTransform3D();
        return Relative ? delta * Transform : Transform;
    }

    protected override Variant Interpolate()
    {
        Transform3D startTransform = start.AsTransform3D();
        Transform3D goalTransform = goal.AsTransform3D();
        return startTransform.InterpolateWith(goalTransform, (float) Percent);
    }
}
using Godot;

namespace Operations;

/// <summary>
/// Scales the target <see cref="Node3D"/>.
/// </summary>
[Operation("NodeScale2D")]
public class NScale2DOperation : NRelativeOperation
{

    /// <summary>
    /// The target scale.
    /// </summary>
    public Vector2 Scale
    {
        get => Value.AsVector2();
        set => Value = value;
    }

    public override void Start()
    {
        Property = Node2D.PropertyName.Scale;
        base.Start();
    }

    protected override Variant DeltaValue()
    {
        Vector2 scale = Value.AsVector2();
        return Relative ? scale : scale - start.AsVector2();
    }
}

/// <summary>
/// Rotates the target <see cref="Node3D"/>.
/// </summary>
[Operation("NodeScale3D")]
public class NScale3DOperation : NRelativeOperation
{

    /// <summary>
    /// The target scale.
    /// </summary>
    public Vector3 Scale
    {
        get => Value.AsVector3();
        set => Value = value;
    }

    public override void Start()
    {
        Property = Node3D.PropertyName.Scale;
        base.Start();
    }

    protected override Variant DeltaValue()
    {
        Vector3 scale = Value.AsVector3();
        return Relative ? scale : scale - start.AsVector3();
    }
}

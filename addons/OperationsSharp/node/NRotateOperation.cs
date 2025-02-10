using Godot;

namespace Operations;

/// <summary>
/// Rotates the target <see cref="Node2D"/>.
/// </summary>
[Operation("NodeRotate2D")]
public class NRotate2DOperation : NRelativeOperation
{

    /// <summary>
    /// The target rotation.
    /// </summary>
    public float RotationDegrees
    {
        get => Value.AsSingle();
        set => Value = value;
    }

    public override void Start()
    {
        Property = Global ? Node2D.PropertyName.GlobalRotationDegrees : Node2D.PropertyName.RotationDegrees;
        base.Start();
    }

    protected override Variant DeltaValue()
    {
        float rotation = Value.AsSingle();
        return Relative ? rotation : rotation - start.AsSingle();
    }
}

/// <summary>
/// Rotates the target <see cref="Node3D"/>.
/// </summary>
[Operation("NodeRotate3D")]
public class NRotate3DOperation : NRelativeOperation
{

    /// <summary>
    /// The target rotation.
    /// </summary>
    public Vector3 RotationDegrees
    {
        get => Value.AsVector3();
        set => Value = value;
    }

    public override void Start()
    {
        Property = Global ? Node3D.PropertyName.GlobalRotationDegrees : Node3D.PropertyName.RotationDegrees;
        base.Start();
    }
    
    protected override Variant DeltaValue()
    {
        Vector3 rotation = Value.AsVector3();
        return Relative ? rotation : rotation - start.AsVector3();
    }
}

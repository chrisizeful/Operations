using Godot;

namespace Operations;

/// <summary>
/// Moves the target <see cref="Node2D"/> to the provided <see cref="Position"/> over time.
/// </summary>
[Operation("NodeMove2D")]
public class NMove2DOperation : NRelativeOperation
{

    /// <summary>
    /// The target 2D position.
    /// </summary>
    public Vector2 Position
    {
        get => Value.AsVector2();
        set => Value = value;
    }

    public override void Start()
    {
        Property = Global ? Node2D.PropertyName.GlobalPosition : Node2D.PropertyName.Position;
        base.Start();
    }

    protected override Variant DeltaValue()
    {
        Vector2 position = Value.AsVector2();
        return Relative ? position : position - start.AsVector2();
    }
}

/// <summary>
/// Moves the target <see cref="Node3D"/> to the provided <see cref="Position"/> over time.
/// </summary>
[Operation("NodeMove3D")]
public class NMove3DOperation : NRelativeOperation
{

    /// <summary>
    /// The target 3D position.
    /// </summary>
    public Vector3 Position
    {
        get => Value.AsVector3();
        set => Value = value;
    }

    public override void Start()
    {
        Property = Global ? Node3D.PropertyName.GlobalPosition : Node3D.PropertyName.Position;
        base.Start();
    }
    
    protected override Variant DeltaValue()
    {
        Vector3 position = Value.AsVector3();
        return Relative ? position : position - start.AsVector3();
    }
}

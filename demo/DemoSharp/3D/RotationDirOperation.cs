using Godot;

namespace Operations;

/// <summary>
/// A custom operation that lerps to a target angle over time.
/// The target angle is calculated to face the specified Position.
/// </summary>
public class RotateDirOperation : TimeOperation
{

    public Vector3 Position;

    float start;
    float rotTarget;

    public override void Start()
    {
        base.Start();
        Node3D target = (Node3D) Target;
        Vector3 delta = target.Position - Position;
        start = target.Rotation.Y;
        rotTarget = Mathf.Atan2(delta.X, delta.Z);
    }

    public override Status Act(double delta)
    {
        // Call base act beforehand instead of just returning it so the Percent will be correct when used.
        Status status = base.Act(delta);
        float rotationY = Mathf.LerpAngle(start, rotTarget, (float) Percent);
        Node3D target = (Node3D) Target;
        target.Rotation = new(0, rotationY, 0);
        return status;
    }
}
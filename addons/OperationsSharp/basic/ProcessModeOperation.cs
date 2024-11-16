using Godot;

namespace Operations;

/// <summary>
/// Sets the <see cref="Node.ProcessModeEnum"/> of the provided <see cref="TargetOperation"/> to <see cref="Set"/>.
/// If no <see cref="TargetOperation"/> is provided, then the <see cref="Node.ProcessModeEnum"/> of this operation
/// is changed.
/// </summary>
public class ProcessModeOperation : Operation
{

    /// <summary>
    /// The operation to change the <see cref="Node.ProcessModeEnum"/> of, or null to set this operations mode.
    /// </summary>
    public Operation TargetOperation;
    /// <summary>
    /// The <see cref="Node.ProcessModeEnum"/> to use.
    /// </summary>
    public Node.ProcessModeEnum Set;

    public override Status Act(double delta)
    {
        Operation operation = TargetOperation ?? this;
        operation.ProcessMode = Set;
        return Status.Succeeded;
    }
}
using Godot;

namespace Operations;

/// <summary>
/// Forces the target node to emit <see cref="Signal"/>.
/// </summary>
[Operation("NodeSignal")]
public class NSignalOperation : TimeOperation
{

    /// <summary>
    /// The name of the signal to emit.
    /// </summary>
    public StringName Signal;
    /// <summary>
    /// Optional arguments to provide the signal.
    /// </summary>
    public Variant[] Args;

    public override Status Act(double delta)
    {
        Node.EmitSignal(Signal, Args);
        return Status.Succeeded;
    }
}

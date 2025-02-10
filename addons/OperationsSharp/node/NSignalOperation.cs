using Godot;

namespace Operations;

/// <summary>
/// Forces the target node to emit the signal named <see cref="SignalName"/>.
/// </summary>
[Operation("NodeSignal")]
public class NSignalOperation : TimeOperation
{

    /// <summary>
    /// The name of the signal to emit.
    /// </summary>
    public StringName SignalName;
    /// <summary>
    /// Optional arguments to provide the signal.
    /// </summary>
    public Variant[] Args;

    public override Status Act(double delta)
    {
        Node.EmitSignal(SignalName, Args);
        return Status.Succeeded;
    }
}

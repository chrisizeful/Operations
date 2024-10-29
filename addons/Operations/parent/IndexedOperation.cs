namespace Operations;

/// <summary>
/// Runs the child operation at the specified <see cref="Index"/>.
/// </summary>
[Operation("Indexed")]
public class IndexedOperation : Operation
{

    /// <summary>
    /// The currently running operation.
    /// </summary>
    public Operation Operation => Children[Index];
    /// <summary>
    /// The index in <see cref="Operation.Children"/> to run.
    /// </summary>
    public int Index;

    public override Status Act(double delta)
    {
        Children[Index].Run(delta);
        return Status.Running;
    }
}
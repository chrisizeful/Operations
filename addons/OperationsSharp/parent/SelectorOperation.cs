namespace Operations;

/// <summary>
/// Fail if all children fail in order, or succeed if one succeeds in the process.
/// </summary>
[Operation("Selector")]
public class SelectorOperation : Operation
{

    /// <summary>
    /// The index of the currently running child.
    /// </summary>
    public int Index { get; private set; }

    public override void Restart()
    {
        base.Restart();
        Index = 0;
    }

    public override void ChildFail() => Index += 1;

    public override Status Act(double delta)
    {
        if (Index >= Children.Count)
            return Status.Failed;
        Children[Index].Run(delta);
        return Status.Running;
    }
}

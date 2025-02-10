namespace Operations;

/// <summary>
/// Runs all children in order, one at a time.
/// </summary>
[Operation("Sequence")]
public class SequenceOperation : Operation
{

    /// <summary>
    /// The policy to use for defining how/when this operation will fail/succeed.
    /// </summary>
    public SequencePolicy Policy = SequencePolicy.All;
    /// <summary>
    /// The currently running operation.
    /// </summary>
    public Operation Operation => Children[Index];
    /// <summary>
    /// The index in <see cref="Operation.Children"/> of the currently running operation.
    /// </summary>
    public int Index { get; private set; }

    public override void Restart()
    {
        base.Restart();
        Index = 0;
    }

    public override void Reset()
    {
        base.Reset();
        Policy = SequencePolicy.All;
    }

    public override void ChildSuccess() => Index += 1;

    public override void ChildFail()
    {
        if (Policy == SequencePolicy.Ignore)
            Index += 1;
        else
            Fail();
    }

    public override Status Act(double delta)
    {
        // Run operation in a single frame like a guard evaluator
        if (delta == 0)
            return Resolve();
        if (Index >= Children.Count)
            return Status.Succeeded;
        Children[Index].Run(delta);
        return Status.Running;
    }

    protected Status Resolve()
    {
        for (int i = 0; i < Children.Count; i++)
        {
            Operation child = Children[i];
            child.Run(0);
            if (child.Current == Status.Failed && Policy != SequencePolicy.Ignore)
                return Status.Failed;
        }
        return Status.Succeeded;
    }
}

/// <summary>
/// Policy for defining how a <see cref="SequenceOperation"/> will behave.
/// </summary>
public enum SequencePolicy
{
    /// <summary>
    /// Succeed if all children succeed in order, fail if one fails in the process.
    /// </summary>
    All,
    /// <summary>
    /// Ignore the return status of children, run all of them in order no matter their return status.
    /// </summary>
    Ignore
}
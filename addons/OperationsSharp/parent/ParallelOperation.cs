namespace Operations;

/// <summary>
/// Runs all child operations at the same time.
/// </summary>
[Operation("Parallel")]
public class ParallelOperation : Operation
{

    /// <summary>
    /// The policy to use for defining how/when this operation will fail/succeed.
    /// </summary>
    public ParallelPolicy Policy = ParallelPolicy.Sequence;
    /// <summary>
    /// How many child operations have returned a passable <see cref="Operation.Status"/> according to the <see cref="Policy"/>.
    /// </summary>
    public int Complete { get; private set; }

    public override void Start()
    {
        base.Start();
        if (Children.Count == 0)
            Success();
    }

    public override void Restart()
    {
        base.Restart();
        Complete = 0;
    }

    public override void Reset()
    {
        base.Reset();
        Policy = ParallelPolicy.Sequence;
    }

    public override void ChildSuccess()
    {
        Complete += 1;
        if (Policy == ParallelPolicy.Sequence)
        {
            if (Complete >= Children.Count)
                Success();
        }
        else if (Policy == ParallelPolicy.Selector || Complete >= Children.Count)
        {
            Success();
        }
    }

    public override void ChildFail()
    {
        Complete += 1;
        if (Policy == ParallelPolicy.Selector)
        {
            if (Complete >= Children.Count)
                Fail();
        }
        else if (Policy == ParallelPolicy.Sequence)
        {
            Fail();
        }
        else if (Complete >= Children.Count)
        {
            Success();
        }
    }
    
    public override Status Act(double delta)
    {
        foreach (Operation child in Children)
            child.Run(delta);
        return Status.Running;
    }
}

/// <summary>
/// Policy for defining how a <see cref="ParallelOperation"/> will behave.
/// </summary>
public enum ParallelPolicy
{
    /// <summary>
    /// Fail if one child fails, succeed if all chidlren succeed.
    /// </summary>
    Sequence,
    /// <summary>
    /// Succeed if one child succeeds, fail if all children fail.
    /// </summary>
    Selector,
    /// <summary>
    /// Run all children to completion, whether they fail or succeed.
    /// </summary>
    Ignore
}
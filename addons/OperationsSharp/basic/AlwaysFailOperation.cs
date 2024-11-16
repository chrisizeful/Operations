namespace Operations;

/// <summary>
/// Returns a <see cref="Operation.Status.Failed"/> status when a child succeeds. If no children exist, a
/// <see cref="Operation.Status.Failed"/> status is immediately set.
/// </summary>
[Operation("AlwaysFail")]
public class AlwaysFailOperation : Operation
{

    public override void Start()
    {
        base.Start();
        if (Children.Count == 0)
            Fail();
    }

    public override Status Act(double delta)
    {
        foreach (Operation child in Children)
            child.Run(delta);
        return Status.Running;
    }

    public override void ChildSuccess() => Fail();

}

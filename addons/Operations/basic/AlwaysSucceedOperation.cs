namespace Operations;

/// <summary>
/// Returns a <see cref="Operation.Status.Succeeded"/> status when a child fails. If no children exist, a
/// <see cref="Operation.Status.Succeeded"/> status is immediately set.
/// </summary>
[Operation("AlwaysSucceed")]
public class AlwaysSucceedOperation : Operation
{

    public override void Start()
    {
        base.Start();
        if (Children.Count == 0)
            Success();
    }

    public override Status Act(double delta)
    {
        foreach (Operation child in Children)
            child.Run(delta);
        return Status.Running;
    }

    public override void ChildFail() => Success();

}

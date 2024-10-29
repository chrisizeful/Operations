namespace Operations;

/// <summary>
/// Runs children until one returns a success status. Ignores failure statuses.
/// </summary>
[Operation("UntilSucceed")]
public class UntilSucceedOperation : Operation
{

    public override Status Act(double delta)
    {
        foreach (Operation child in Children)
            child.Run(delta);
        return Status.Running;
    }

    public override void ChildSuccess() => Success();

    // Ignore child fail
    public override void ChildFail() {}
}

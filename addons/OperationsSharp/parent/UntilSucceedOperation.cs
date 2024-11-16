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

    // Ignore child fail
    public override void ChildFail() {}
}

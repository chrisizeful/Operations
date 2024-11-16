namespace Operations;

/// <summary>
/// Runs children until one returns a failure status. Ignores success statuses.
/// </summary>
[Operation("UntilFail")]
public class UntilFailOperation : Operation
{

    public override Status Act(double delta)
    {
        foreach (Operation child in Children)
            child.Run(delta);
        return Status.Running;
    }

    // Ignore child success
    public override void ChildSuccess() {}

    public override void ChildFail() => Success();

}

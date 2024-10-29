namespace Operations;

/// <summary>
/// Always returns a <see cref="Operation.Status.Running"/> status.
/// </summary>
[Operation("AlwaysRunning")]
public class AlwaysRunningOperation : Operation
{

    public override void ChildSuccess() {}
    public override void ChildFail() {}

    public override Status Act(double delta) => Status.Running;

}
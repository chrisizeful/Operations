namespace Operations;

/// <summary>
/// Fails when a child succeeds and succeeds when a child fails, inverting the result.
/// </summary>
[Operation("Invert")]
public class InvertOperation : Operation
{

    public override Status Act(double delta)
    {
        Children[0].Run(delta);
        return Status.Running;
    }

    public override void ChildSuccess() => Fail();
    public override void ChildFail() => Success();
}

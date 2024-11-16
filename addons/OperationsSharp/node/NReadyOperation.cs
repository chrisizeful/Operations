namespace Operations;

/// <summary>
/// Waits for the target node to be ready until returning <see cref="Operation.Status.Succeeded"/>.
/// </summary>
[Operation("NodeReady")]
public class NReadyOperation : Operation
{

    public override Status Act(double delta)
    {
        return Node.IsNodeReady() ? Status.Succeeded : Status.Running;
    }
}

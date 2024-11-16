namespace Operations;

/// <summary>
/// Does not continuously set it's own status like other operations, instead it must be manually set by the user.
/// </summary>
public class ManualOperation : Operation
{

    public override void Run(double delta)
    {
        // Return if cancelled, failed, or succeeded
        if (Current is not Status.Running and not Status.Fresh)
            return;
        // Check if operation is fresh
        if (Current is Status.Fresh)
        {
            Start();
            Current = Status.Running;
        }
    }
}
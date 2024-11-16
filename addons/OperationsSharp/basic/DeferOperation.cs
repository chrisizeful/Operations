namespace Operations;

/// <summary>
/// Returns the <see cref="Operation.Status"/> of the provided <see cref="Operation"/>. Or, if the provided operation
/// is null, immediately fails.
/// </summary>
[Operation("Defer")]
public class DeferOperation : Operation
{

    /// <summary>
    /// The operation whose status will be used.
    /// </summary>
    public Operation Operation;

    public override void Start()
    {
        base.Start();
        if (Operation == null)
            Fail();
    }

    public override Status Act(double delta)
    {
        return Operation.Current;
    }
}

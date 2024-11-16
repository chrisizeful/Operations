namespace Operations;

/// <summary>
/// Repeats the first child operation <see cref="Limit"/> times.
/// </summary>
[Operation("Repeat")]
public class RepeatOperation : Operation
{

    /// <summary>
    /// The number of times to run the child operation, or zero for infinite times.
    /// </summary>
    public int Limit = 1;
    /// <summary>
    /// How many times the child operation has been repeated.
    /// </summary>
    public int Count { get; private set; } = 0;

    public override void Restart()
    {
        base.Restart();
        Count = 0;
    }

    public override Status Act(double delta)
    {
        Children[0].Run(delta);
        return Status.Running;
    }

    public override void ChildSuccess()
    {
        Count++;
        if (Limit != 0 && Count >= Limit)
            Success();
        else
            Restart();
    }
}

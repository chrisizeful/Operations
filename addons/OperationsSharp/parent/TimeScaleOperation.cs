namespace Operations;

/// <summary>
/// Runs children with a delta value scaled by <see cref="Scale"/>.
/// </summary>
[Operation("TimeScale")]
public class TimeScaleOperation : Operation
{

    /// <summary>
    /// The value to scale time (delta) by.
    /// </summary>
    public float Scale = 1.0f;

    public override Status Act(double delta)
    {
        foreach (Operation child in Children)
            child.Run(delta * Scale);
        return Status.Running;
    }
}

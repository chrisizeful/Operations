namespace Operations;

/// <summary>
/// Waits a specific <see cref="Duration"/> in seconds.
/// </summary>
[Operation("Time")]
public class TimeOperation : Operation
{

    /// <summary>
    /// The time in seconds to wait.
    /// </summary>
    public double Duration;
    /// <summary>
    /// Whether <see cref="Percent"/> will be reversed (1.0 - 0.0 instead of 0.0 - 1.0).
    /// </summary>
    public bool Reverse;
    
    /// <summary>
    /// How much time has elapsed so far.
    /// </summary>
    public double Time { get; private set; }
    /// <summary>
    /// The percentage based on <see cref="Time"/> / <see cref="Duration"/>.
    /// </summary>
    public double Percent { get; private set; }
    
    public override void Restart()
    {
        base.Restart();
        Time = Percent = 0;
    }

    public override Status Act(double delta)
    {
        Time += delta;
        double percent = Percent = Time >= Duration ? 1 : Time / Duration;
        if (Reverse)
            Percent = 1 - Percent;
        if (percent == 1 && Children.Count != 0)
        {
            Children[0].Run(delta);
            return Status.Running;
        }
        return percent == 1 ? Status.Succeeded : Status.Running;
    }
}

/// <summary>
/// Acts on relative time changes between frames instead of the overall complete percentage.
/// </summary>
public abstract class RelativeTimeOperation : TimeOperation
{

    private double _lastPercent;

    public override void Restart()
    {
        base.Restart();
        _lastPercent = 0;
    }

    public override Status Act(double delta)
    {
        Status status = base.Act(delta);
        ActRelative(Percent - _lastPercent);
        _lastPercent = Percent;
        return status;
    }

    protected abstract void ActRelative(double percentDelta);

}

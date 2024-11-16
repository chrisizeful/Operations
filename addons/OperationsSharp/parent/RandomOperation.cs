using System;

namespace Operations;

/// <summary>
/// Once the child operation is complete, randomly fail or succeed.
/// If there are children, it will randomly fail or succeed immediately.
/// </summary>
[Operation("Random")]
public class RandomOperation : Operation
{

    /// <summary>
    /// The probability of returning a <see cref="Operation.Status.Succeeded"/> status.
    /// </summary>
    public double Probability = .5;
    /// <summary>
    /// The <see cref="Random"/> object to use, or null.
    /// </summary>
    public Random Rand;

    public override void Start()
    {
        base.Start();
        Rand ??= new Random();
    }

    public override Status Act(double delta)
    {
        if (Children.Count > 0)
            Children[0].Run(delta);
        else
            Decide();
        return Status.Running;
    }

    public override void ChildFail() => Decide();
    public override void ChildSuccess() => Decide();

    private void Decide()
    {
        float value = (float) Rand.NextDouble();
        if (value <= Probability) Success();
        else Fail();
    }
}

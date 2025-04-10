using System;

namespace Operations;

/// <summary>
/// Delegates status to a function.
/// </summary>
public class FuncOperation : Operation
{

    /// <summary>
    /// The function that returns/determines the operation status.
    /// </summary>
    public Func<Status> Func;

    public override Status Act(double delta)
    {
        return Func == null ? Status.Failed : Func();
    }
}
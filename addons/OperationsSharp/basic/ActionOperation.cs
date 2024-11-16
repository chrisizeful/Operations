using System;

namespace Operations;

/// <summary>
/// Invokes an the given <see cref="Action"/> then immediately returns a <see cref="Operation.Status.Succeeded"/> status.
/// </summary>
public class ActionOperation : Operation
{

    /// <summary>
    /// The action to invoke.
    /// </summary>
    public Action Action;

    public override Status Act(double delta)
    {
        Action.Invoke();
        return Status.Succeeded;
    }
}

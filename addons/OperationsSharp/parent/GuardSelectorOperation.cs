namespace Operations;

/// <summary>
/// An operation for defining complex behaviors using guard operations. Every run,
/// it will check the guards of all its children. The first child operation whose guard
/// passes (either because it succeeded or the operation lacks a guard) will be run.
/// - If all guards fail, this operation fails.
/// - If the <see cref="RunningChild"/> guard fails next frame, the <see cref="RunningChild"/> is cancelled.
/// - If <see cref="RunningChild"/> does not have a guard and succeeds/fails, this operation succeeds/fails.
/// </summary>
[Operation("GuardSelector")]
public class GuardSelectorOperation : Operation
{

    /// <summary>
    /// The last child whose guard passed and was run.
    /// </summary>
    public Operation RunningChild { get; private set; }

    public override Operation Display()
    {
        RunningChild?.Display();
        return this;        
    }

    public override void Restart()
    {
        base.Restart();
        RunningChild = null;
    }

    public override Status Act(double delta)
    {
        // Check all child guards
        Operation childToRun = null;
        foreach (Operation operation in Children)
        {
            if (operation.CheckGuard())
            {
                childToRun = operation;
                break;
            }
        }
        // Check if childToRun changed
        if (RunningChild != null && RunningChild != childToRun)
        {
            RunningChild.Cancel();
            RunningChild = null;
        }
        // If no child was found to run, fail
        if (childToRun == null)
            return Status.Failed;
        // Set and run running child
        RunningChild ??= childToRun;
        RunningChild.Run(delta);
        return Status.Running;
    }

    public override void ChildSuccess()
    {
        // Succeed if running child succeeded
        if (RunningChild?.Current is Status.Succeeded)
            Success();
    }

    public override void ChildFail()
    {
        // Fail if running child fails
        if (RunningChild?.Current is Status.Failed)
            Fail();
    }
}

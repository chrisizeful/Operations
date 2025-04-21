namespace Operations;

/// <summary>
/// Uses the <see cref="Operation.TargetValidator"/> to determine if the <see cref="Operation.Target"/>
/// is valid. Fail if invalid, otherwise succeed.
/// </summary>
public class ValidOperation : Operation
{

    public override Status Act(double delta)
    {
        if (!TargetValidator.Invoke(this))
            return Status.Failed;
        return Status.Succeeded;
    }
}
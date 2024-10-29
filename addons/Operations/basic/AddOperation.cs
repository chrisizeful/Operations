namespace Operations;

/// <summary>
/// Adds the provided <see cref="Operation"/> to the <see cref="Operator" then immediately returns a <see cref="Operation.Status.Succeeded"/> status. 
/// </summary>
public class AddOperation : Operation
{

    /// <summary>
    /// The Operator to add to the <see cref="Operation"/> to. 
    /// </summary>
    public Operator Operator;
    /// <summary>
    /// The Operation to add to the <see cref="Operator"/>.
    /// </summary>
    public Operation Operation;

    public override Status Act(double delta)
    {
        Operator?.Add(Operation);
        return Status.Succeeded;
    }
}

using Godot;

namespace Operations;

/// <summary>
/// Sets a property in the target node then immediatley returns <see cref="Operation.Status.Succeeded"/>.
/// </summary>
[Operation("NodeSet")]
public class NSetOperation : TimeOperation
{

    /// <summary>
    /// The name of the property to set.
    /// </summary>
    public StringName Property;
    /// <summary>
    /// The value that <see cref="Property"/> will be set to.
    /// </summary>
    public Variant Value;

    public override Status Act(double delta)
    {
        Node.Set(Property, Value);
        return Status.Succeeded;
    }
}

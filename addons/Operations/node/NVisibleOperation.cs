namespace Operations;

/// <summary>
/// A convenience operation to toggle the visibility of a node.
/// </summary>
[Operation("NodeVisible")]
public class NVisibleOperation : Operation
{

    /// <summary>
    /// Whether the target will be visible.
    /// </summary>
    public bool Visible;

    public override Status Act(double delta)
    {
        Node.Set("visible", Visible);
        return Status.Succeeded;
    }
}

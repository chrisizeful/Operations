using Godot;

namespace Operations;

/// <summary>
/// Calls <see cref="Node.QueueFree"/> on the target and immediatley returns <see cref="Operation.Status.Succeeded"/>.
/// </summary>
[Operation("NodeFree")]
public class NFreeOperation : Operation
{

    public override Status Act(double delta)
    {
        // FIXME Remove from parent first to ensure the TreeExited and TreeExiting signals fire.
        Node node = Node;
        node.GetParent()?.RemoveChild(node);
        node.QueueFree();
        return Status.Succeeded;
    }
}

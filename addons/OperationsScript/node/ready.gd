class_name NReadyOperation
extends Operation
## Waits for the target node to be ready until returning Status.Succeeded.

func act(delta : float) -> Status:
	return Status.Succeeded if node.is_node_ready() else Status.Running

class_name NVisibleOperation
extends Operation
## A convenience operation to toggle the visibility of a node.

## Whether the target will be visible.
var visible : bool

func act(delta : float) -> Status:
	target.set("visible", visible)
	return Status.Succeeded

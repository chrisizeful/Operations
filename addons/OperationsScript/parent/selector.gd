class_name SelectorOperation
extends Operation
## Fail if all children fail in order, or succeed if one succeeds in the process.

## The index of the currently running child.
var index : int

func restart():
	super.restart()
	index = 0

func child_fail():
	index += 1

func act(delta : float) -> Status:
	if index >= children.size():
		return Status.Failed
	children[index].run(delta)
	return Status.Running

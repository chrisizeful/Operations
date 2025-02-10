class_name SelectorOperation
extends Operation
## Fail if all children fail in order, or succeed if one succeeds in the process.

var _index : int
## The index of the currently running child.
var index : int:
	get:
		return _index

func restart():
	super.restart()
	_index = 0

func child_fail():
	_index += 1

func act(delta : float) -> Status:
	if _index >= children.size():
		return Status.Failed
	children[_index].run(delta)
	return Status.Running

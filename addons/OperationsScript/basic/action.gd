class_name ActionOperation
extends Operation

var action : Callable

func act(delta : float) -> Status:
	action.call()
	return Status.Succeeded

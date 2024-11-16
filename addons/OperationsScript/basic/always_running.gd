class_name AlwaysRunningOperation
extends Operation
## Always returns a <see cref="Operation.Status.Running"/> status.

func child_success():
	pass

func child_fail():
	pass

func act(delta : float) -> Status:
	return Status.Running

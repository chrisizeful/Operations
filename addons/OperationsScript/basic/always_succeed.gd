class_name AlwaysSucceedOperation
extends Operation
## Returns a <see cref="Operation.Status.Succeeded"/> status when a child fails. If no children exist, a
## <see cref="Operation.Status.Succeeded"/> status is immediately set.

func start():
	super.start()
	if children.size() == 0:
		success()

func act(delta : float) -> Status:
	for child in children:
		child.run(delta)
	return Status.Running

func child_fail():
	success()

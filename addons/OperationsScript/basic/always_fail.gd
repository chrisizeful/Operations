class_name AlwaysFailOperation
extends Operation
## Returns a <see cref="Operation.Status.Failed"/> status when a child succeeds. If no children exist, a
## <see cref="Operation.Status.Failed"/> status is immediately set.

func start():
	super.start()
	if children.size() == 0:
		fail()

func act(delta : float) -> Status:
	for child in children:
		child.run(delta)
	return Status.Running

func child_success():
	fail()

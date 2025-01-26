class_name NFreeOperation
extends Operation
## Calls <see cref="Node.QueueFree"/> on the target and immediatley returns <see cref="Operation.Status.Succeeded"/>.

func act(delta : float) -> Status:
	# FIXME Remove from parent first to ensure the TreeExited and TreeExiting signals fire.
	var parent = target.get_parent()
	if parent:
		parent.remove_child(target)
	target.queue_free()
	return Status.Succeeded

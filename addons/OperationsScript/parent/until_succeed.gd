class_name UntilSucceedOperation
extends Operation
## Runs children until one returns a success status. Ignores failure statuses.

func act(delta : float) -> Status:
	for child in children:
		child.run(delta)
	return Status.Running

## Ignore child fail
func child_fail():
	pass

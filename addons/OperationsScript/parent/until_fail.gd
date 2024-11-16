class_name UntilFailOperation
extends Operation
## Runs children until one returns a failure status. Ignores success statuses.

func act(delta : float) -> Status:
	for child in children:
		child.run(delta)
	return Status.Running

## Ignore child success
func child_success():
	pass

func child_fail():
	success()

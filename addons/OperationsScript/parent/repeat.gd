class_name RepeatOperation
extends Operation
## The number of times to run the child operation, or zero for infinite times.

## The number of times to run the child operation, or zero for infinite times.
var limit := 1
var _count : int
## How many times the child operation has been repeated.
var count : int:
	get:
		return _count

func restart():
	super.restart()
	_count = 0

func act(delta : float) -> Status:
	children[0].run(delta)
	return Status.Running

func child_success():
	_count += 1
	if limit != 0 and _count >= limit:
		success()
	else:
		restart()

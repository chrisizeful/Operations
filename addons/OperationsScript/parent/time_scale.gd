class_name TimeScaleOperation
extends Operation
## Runs children with a delta value scaled by <see cref="Scale"/>.

## The value to scale time (delta) by.
var scale : float

func act(delta : float) -> Status:
	for child in children:
		child.run(delta * scale)
	return Status.Running

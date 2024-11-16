class_name IndexedOperation
extends Operation
## Runs the child operation at the specified <see cref="Index"/>.

## Runs the child operation at the specified <see cref="Index"/>.
var operation:
	get:
		children[index]
## The index in <see cref="Operation.Children"/> to run.
var index : int

func act(delta : float) -> Status:
	children[index].run(delta)
	return Status.Running

class_name DeferOperation
extends Operation
## Returns the <see cref="Operation.Status"/> of the provided <see cref="Operation"/>. Or, if the provided operation
## is null, immediately fails.

## The operation whose status will be used.
var operation : Operation

func start():
	super.start()
	if !operation:
		fail()

func act(delta : float) -> Status:
	return operation.current

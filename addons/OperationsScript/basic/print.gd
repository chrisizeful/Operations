class_name PrintOperation
extends Operation
## Prints the provided <see cref="What"/> to the console.

## What to print.
var what

func act(delta : float) -> Status:
	print(what)
	return Status.Succeeded

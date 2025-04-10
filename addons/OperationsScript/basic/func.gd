class_name FuncOperation
extends Operation
### Delegates status to a function.

### The function that returns/determines the operation status.
var sfunc : Callable

func act(delta : float) -> Status:
	return sfunc.call()

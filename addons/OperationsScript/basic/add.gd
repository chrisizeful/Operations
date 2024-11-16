class_name AddOperation
extends Operation
## Adds the provided <see cref="Operation"/> to the <see cref="Operator" then immediately returns a <see cref="Operation.Status.Succeeded"/> status. 

## The Operator to add to the <see cref="Operation"/> to. 
var operator : Operator
## The Operation to add to the <see cref="Operator"/>.
var operation : Operation

func act(delta : float) -> Status:
	if operator:
		operator.add(operation)
	return Status.Succeeded

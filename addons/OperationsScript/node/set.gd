class_name NSetOperation
extends Operation
## Sets a property in the target node then immediatley returns Status.Succeeded.

## The name of the property to set.
var property : StringName
## The value that <see cref="Property"/> will be set to.
var value : Variant

func act(delta : float) -> Status:
	node.set(property, value)
	return Operation.Status.Succeeded

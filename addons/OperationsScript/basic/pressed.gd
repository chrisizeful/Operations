class_name PressedOperation
extends Operation
## Returns <see cref="Operation.Status.Succeeded"/> if the provided <see cref="Action"/> is pressed.

## The action to await input for.
var action : StringName

func act(delta : float) -> Status:
	if Input.is_action_pressed(action):
		return Status.Succeeded
	return Status.Running

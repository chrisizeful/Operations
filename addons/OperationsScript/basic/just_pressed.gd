class_name JustPressedOperation
extends Operation
## Returns <see cref="Operation.Status.Succeeded"/> if the provided <see cref="Action"/> is just pressed.

## The action to await input for.
var action : StringName

func act(delta : float) -> Status:
	if Input.is_action_just_pressed(action):
		return Status.Succeeded
	return Status.Running

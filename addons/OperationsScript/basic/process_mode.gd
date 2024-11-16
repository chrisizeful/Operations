class_name ProcessModeOperation
extends Operation
##  Sets the <see cref="Node.ProcessModeEnum"/> of the provided <see cref="TargetOperation"/> to <see cref="Set"/>.
## If no <see cref="TargetOperation"/> is provided, then the <see cref="Node.ProcessModeEnum"/> of this operation
## is changed.

## The operation to change the <see cref="Node.ProcessModeEnum"/> of, or null to set this operations mode.
var target_operation : Operation
## The <see cref="Node.ProcessModeEnum"/> to use.
var set_mode : Node.ProcessMode

func act(delta : float) -> Status:
	var operation = target_operation
	if !operation:
		operation = self
	operation.process_mode = set_mode
	return Status.Succeeded

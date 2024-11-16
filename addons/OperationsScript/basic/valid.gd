class_name ValidOperation
extends Operation
## Uses the <see cref="Operation.TargetValidator"/> to determine if the <see cref="Operation.Target"/>
## is valid. Fail if invalid, otherwise succeed.

func act(delta : float) -> Status:
	if !target_validator.call(target):
		return Status.Failed
	return Status.Succeeded

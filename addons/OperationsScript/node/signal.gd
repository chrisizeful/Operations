class_name NSignalOperation
extends Operation
## Forces the target node to emit the signal named SignalName

## The name of the signal to emit.
var signal_name : StringName

func act(delta : float) -> Status:
	node.emit_signal(signal_name)
	return Operation.Status.Succeeded

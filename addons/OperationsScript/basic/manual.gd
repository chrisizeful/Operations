class_name ManualOperation
extends Operation
## Does not continuously set it's own status like other operations, instead it must be manually set by the user.

func run(delta : float):
	# Return if cancelled, failed, or succeeded
	if current != Status.Running and current != Status.Fresh:
		return
	# Check if operation is fresh
	if current == Status.Fresh:
		start()
		current = Status.Running

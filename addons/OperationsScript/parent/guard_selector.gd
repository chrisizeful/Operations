class_name GuardSelectorOperation
extends Operation
## An operation for defining complex behaviors using guard operations. Every run,
## it will check the guards of all its children. The first child operation whose guard
## passes (either because it succeeded or the operation lacks a guard) will be run.
## - If all guards fail, this operation fails.
## - If the <see cref="RunningChild"/> guard fails next frame, the <see cref="RunningChild"/> is cancelled.
## - If <see cref="RunningChild"/> does not have a guard and succeeds/fails, this operation succeeds/fails.

var _running_child : Operation
## The last child whose guard passed and was run.
var running_child : Operation:
	get:
		return _running_child

func display() -> Operation:
	_running_child.display()
	return self

func restart():
	super.restart()
	_running_child = null

func act(delta : float) -> Status:
	# Check all child guards
	var child_to_run : Operation
	for operation in children:
		if operation.check_guard():
			child_to_run = operation
			break
	# Check if child_to_run changed
	if _running_child and _running_child != child_to_run:
		_running_child.cancel()
		_running_child = null
	# If no child was found to run, fail
	if !child_to_run:
		return Status.Failed
	# Set and run running child
	if !_running_child:
		_running_child = child_to_run
	_running_child.run(delta)
	return Status.Running

func child_success():
	# Succeed if running child succeeded
	if _running_child and _running_child.current == Status.Succeeded:
		success()

func child_fail():
	# Fail if running child fails
	if _running_child and _running_child.current == Status.Failed:
		fail()

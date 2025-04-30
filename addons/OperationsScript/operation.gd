class_name Operation
extends Object
## The base class for all operations.

## An optional name, can be used for debugging purposes.
var name : String
##  The operation that controls/parents this one.
var control : Operation
## The operation that guards this one (i.e. this operation will not process unless this guard succeeds).
var guard : Operation
## Children this operation is responsible for processing.
var children : Array[Operation]
## The object to operate on.
var target : Object
## A cached validator used to validate <see cref="Target"/>.
## Automatically set when <see cref="SetTarget"/> is called.
var target_validator : Callable
## The Node to operate on, provided for convenience.
var node : Node:
	get:
		return target as Node
## The current <see cref="Status"/> of this operation.
var current : Status
## How this operation behaves when the <see cref="SceneTree"/> is paused.
var process_mode : Node.ProcessMode
## What happens if the <see cref="Target"/> is invalidated (i.e. freed from memory).
var invalid : InvalidPolicy

func set_name(name : String) -> Operation:
	self.name = name
	return self

## Sets the operation that will "guard" this one. If the guard does not have a target,
## it's target will be set to this operations target.
## <param name="guard">The operation that needs to succeed.</param>
## <returns>This operation for chaining.</returns>
func set_guard(guard : Operation) -> Operation:
	self.guard = guard
	if !guard.target:
		guard.set_target(target)
	return self

func set_target(target : Object, force : bool = false) -> Operation:
	self.target = target
	target_validator = Operator.target_validators[typeof(target)]
	# Set target for guard
	if guard != null and (guard.target == null || force):
		guard.set_target(target, force)
	# Set target for children
	for child in children:
		if target != null and (child.target == null || force):
			child.set_target(target, force)
	return self

func set_process_mode(mode : Node.ProcessMode) -> Operation:
	# Inherit target process mode
	if mode == Node.ProcessMode.PROCESS_MODE_INHERIT and target:
		if target is Node:
			mode = (target as Node).process_mode
		else:
			mode = Node.ProcessMode.PROCESS_MODE_ALWAYS
	process_mode = mode
	# Set mode for guard
	if guard:
		guard.set_process_mode(mode)
	for child in children:
		if target:
			child.set_process_mode(mode)
	return self

## Sets how this operation behaves when the <see cref="Target"/> is invalidated (i.e. freed from memory).
## <param name="invalid">The policy to use.</param>
## <returns>This operation for chaining.</returns>
func set_invalid_policy(invalid : InvalidPolicy) -> Operation:
	self.invalid = invalid
	if guard:
		guard.set_invalid_policy(invalid)
	for child in children:
		child.set_invalid_policy(invalid)
	return self

## A convenience method to add a child Operation.
## <param name="child">The child to add.</param>
## <returns>This operation for chaining.</returns>
func add_child(child : Operation) -> Operation:
	children.append(child)
	return self

## Sets the operation back to its initial state so it can be run again.
func restart():
	# Cancel but reset status
	cancel()
	current = Status.Fresh
	if guard:
		guard.restart()
	for child in children:
		child.restart()

## A mostly unused method that can be used for debug or other purposes.
## <returns>This operation for chaining.</returns>
func display() -> Operation:
	for child in children:
		child.display()
	return self

## Called once when operation is first run (i.e. <see cref="Current"/> is <see cref="Status.Fresh"/>).
func start():
	for child in children:
		child.control = self

## Called when the operation succeeds, fails, or is cancelled.
func end():
	pass

## Called when when <see cref="Current"/> is <see cref="Status.Running"/>.
func running():
	current = Status.Running
	if control:
		control.child_running()

## Called when <see cref="Current"/> is <see cref="Status.Succeeded"/>.
func success():
	if current == Status.Succeeded:
		return
	current = Status.Succeeded
	if control:
		control.child_success()
	end()

## Called when <see cref="Current"/> is <see cref="Status.Failed"/>.
func fail():
	if current == Status.Failed:
		return
	current = Status.Failed
	if control:
		control.child_fail()
	end()

## Called when <see cref="Current"/> is <see cref="Status.Cancelled"/>.
func cancel():
	if current != Status.Fresh || current != Status.Running:
		return
	current = Status.Cancelled
	for child in children:
		child.cancel()
	end()

## Called when a child operation succeeds.
func child_success():
	success()

## Called when a child operation fails.
func child_fail():
	fail()

## Called when a child operation is running.
func child_running():
	pass

## Checks whether or not the <see cref="Guard"/> succeeds. If no guard is present,
## returns true.
## <returns>If the <see cref="Guard" succeeded./></returns>
func check_guard() -> bool:
	# No guard to check
	if !guard:
		return true
	# CHeck the guard of the guard recursively
	if !guard.check_guard():
		return false
	# Use the tree's guard evaluator task to check the guard of this task
	guard.run(0)
	return guard.Current == Status.Succeeded

## Runs this operation if <see cref="Current"/> is one of <see cref="Status.Fresh"/> or <see cref="Status.Running"/>.
## This method is responsible for updating the <see cref="Current"/> status and calling the relevant methods (i.e. <see cref="Fail"/>)
## <param name="delta">Delta time between frames.</param>
func run(delta : float):
	# Return if cancelled, or succeeded
	if current != Status.Running && current != Status.Fresh:
		return
	# Fail if no validator or target is not valid
	if !target_validator || !target_validator.call(self):
		return
	# Check if operation is fresh
	if current == Status.Fresh:
		start()
	# Act
	var result = act(delta)
	match (result):
		Status.Succeeded:
			success()
		Status.Failed:
			fail()
		Status.Running:
			running()

## The core logic of an operation which determines its status.
## <param name="delta">Delta time between frames.</param>
## <returns>The current status.</returns>
func act(delta : float) -> Status:
	return Status.Running

## Resets the operation to a blank state so it can be reused.
func reset():
	restart()
	control = null
	target = null
	process_mode = Node.PROCESS_MODE_PAUSABLE
	children.clear()

## An enum for determining the status after an operation is run.
enum Status {
	## Has never run or has been reset.
	Fresh,
	## Is in the middle of operating.
	Running,
	## Ran and returned a failure result.
	Failed,
	## Ran and returned a success result.
	Succeeded,
	## Operation was terminated before returning a success or failure.
	Cancelled
}

## Policy for what happens if the <see cref="Target"/> is invalidated (i.e. freed from memory).
enum InvalidPolicy {
	## Ignore the fact the target is invalided and return a success status.
	Success,
	## Fail when the target is invalidated.
	Fail
}

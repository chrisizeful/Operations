class_name SequenceOperation
extends Operation
## Runs all children in order, one at a time.

## The policy to use for defining how/when this operation will fail/succeed.
var policy := SequencePolicy.All
## The currently running operation
var operation:
	get:
		return children[index]
var _index : int
## The index in <see cref="Operation.Children"/> of the currently running operation.
var index : int:
	get:
		return _index

func restart():
	super.restart()
	_index = 0

func reset():
	super.reset()
	policy = SequencePolicy.All

func child_success():
	_index += 1

func child_fail():
	if policy == SequencePolicy.Ignore:
		_index += 1
	else:
		fail()

func act(delta : float) -> Status:
	# Run operation in a single frame like a guard evaluator
	if delta == 0:
		return _resolve()
	if _index >= children.size():
		return Status.Succeeded
	children[_index].run(delta)
	return Status.Running

func _resolve() -> Status:
	for child in children:
		child.run(0)
		if child.current == Status.Failed and policy != SequencePolicy.Ignore:
			return Status.Failed
	return Status.Succeeded

## Policy for defining how a <see cref="SequenceOperation"/> will behave.
enum SequencePolicy {
	## Succeed if all children succeed in order, fail if one fails in the process.
	All,
	## Ignore the return status of children, run all of them in order no matter their return status.
	Ignore,
}

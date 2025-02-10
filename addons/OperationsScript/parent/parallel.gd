class_name ParallelOperation
extends Operation
## Runs all child operations at the same time.

## The policy to use for defining how/when this operation will fail/succeed.
var policy := ParallelPolicy.Sequence
var _complete : int
## How many child operations have returned a passable <see cref="Operation.Status"/> according to the <see cref="Policy"/>.
var complete : int:
	get:
		return _complete

func start():
	super.start()
	if children.size() == 0:
		success()

func restart():
	super.restart()
	_complete = 0

func reset():
	super.reset()
	policy = ParallelPolicy.Sequence

func child_success():
	_complete += 1
	if policy == ParallelPolicy.Sequence:
		if _complete >= children.size():
			success()
	elif policy == ParallelPolicy.Selector or _complete >= children.size():
		success()

func child_fail():
	_complete += 1
	if policy == ParallelPolicy.Selector:
		if _complete >= children.size():
			fail()
	elif policy == ParallelPolicy.Sequence:
		fail()
	elif _complete >= children.size():
		success()

func act(delta : float) -> Status:
	for child in children:
		child.run(delta)
	return Status.Running

## Policy for defining how a <see cref="ParallelOperation"/> will behave.
enum ParallelPolicy {
	## Fail if one child fails, succeed if all chidlren succeed.
	Sequence,
	## Succeed if one child succeeds, fail if all children fail.
	Selector,
	## Run all children to completion, whether they fail or succeed.
	Ignore
}

class_name RandomOperation
extends Operation
## Once the child operation is complete, randomly fail or succeed.
## If there are children, it will randomly fail or succeed immediately.

## The probability of returning a <see cref="Operation.Status.Succeeded"/> status.
var probability := .5
## The <see cref="RandomNumberGenerator"/> object to use, or null.
var rand : RandomNumberGenerator

func start():
	super.start()
	if !rand:
		rand = RandomNumberGenerator.new()

func act(delta : float) -> Status:
	if children.size() > 0:
		children[0].run(delta)
	else:
		_decide()
	return Status.Running

func child_fail():
	_decide()

func child_success():
	_decide()

func _decide():
	var value := rand.randf()
	if value <= probability:
		success()
	else:
		fail()

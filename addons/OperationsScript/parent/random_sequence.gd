class_name RandomSequenceOperation
extends Operation
## Randomly sets an order to run its children in. Succeed if all children succeed in order,
## or fail if one fails in the process.

## The <see cref="RandomNumberGenerator"/> object to use, or null.
var rand : RandomNumberGenerator

var _sequence : Array[int]
var _index := 0

func start():
	super.start()
	_index = 0
	_set_sequence()

func _set_sequence():
	_sequence.clear()
	for i in range(children.size()):
		if !_sequence.find(i):
			_sequence.append(i)
	_shuffle()

func _shuffle():
	if !rand:
		rand = RandomNumberGenerator.new()
	var count := _sequence.size()
	var last := count - 1
	for i in range(last):
		var r = rand.randi_range(i, count)
		var temp = _sequence[r]
		_sequence[r] = _sequence[i]
		_sequence[i] = temp

func child_success():
	_set_sequence()
	_index += 1

func act(delta : float) -> Status:
	if _index >= _sequence.size():
		return Status.Failed
	children[_sequence[_index]].run(delta)
	return Status.Running

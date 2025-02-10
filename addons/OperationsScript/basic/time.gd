class_name TimeOperation
extends Operation
## Waits a specific <see cref="Duration"/> in seconds.

## The time in seconds to wait.
var duration : float
## Whether <see cref="Percent"/> will be reversed (1.0 - 0.0 instead of 0.0 - 1.0).
var reverse : bool

var _time : float
## How much time has elapsed so far.
var time : float:
	get:
		return _time
var _percent : float
## The percentage based on <see cref="Time"/> / <see cref="Duration"/>.
var percent : float:
	get:
		return _percent

func restart():
	super.restart()
	_time = 0
	_percent = 0

func act(delta : float) -> Status:
	_time += delta
	var percent = 1 if _time >= duration else _time / duration
	_percent = percent
	if reverse:
		_percent = 1 - _percent
	if percent == 1 and children.size() != 0:
		children[0].run(delta)
		return Status.Running
	return Status.Succeeded if percent == 1 else Status.Running

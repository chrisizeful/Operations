class_name TimeOperation
extends Operation
## Waits a specific <see cref="Duration"/> in seconds.

## The time in seconds to wait.
var duration : float
## Whether <see cref="Percent"/> will be reversed (1.0 - 0.0 instead of 0.0 - 1.0).
var reverse : bool

## How much time has elapsed so far.
var time : float
## The percentage based on <see cref="Time"/> / <see cref="Duration"/>.
var percent : float

func restart():
	super.restart()
	time = 0
	percent = 0

func act(delta : float) -> Status:
	print(percent)
	time += delta
	var percent = 1 if time >= duration else time / duration
	self.percent = percent
	if reverse:
		self.percent = 1 - self.percent
	if percent == 1 and children.size() != 0:
		children[0].run(delta)
		return Status.Running
	return Status.Succeeded if percent == 1 else Status.Running

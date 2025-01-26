class_name NMethodOperation
extends Operation
## Interpolates on a Callable (method) over time from <see cref="From"/> to <see cref="To"/>.

## The method to interpolate.
var method : Callable
## The starting value.
var from
## The ending value.
var to
## How long it will take to get from <see cref="From"/> to <see cref="To"/>.
var duration : float

var trans_type := Tween.TransitionType.TRANS_LINEAR
var ease_type := Tween.EaseType.EASE_IN_OUT

var _tween : Tween

func start():
	super.start()
	_tween = target.create_tween()
	_tween.tween_method(method, from, to , duration)
	_tween.bind_node(target)
	_tween.finished.connect(success)

func restart():
	super.restart()
	if is_instance_valid(_tween):
		_tween.cancel_free()

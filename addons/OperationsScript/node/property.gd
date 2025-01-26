class_name NPropertyOperation
extends TimeOperation
## Interpolates on a property over time from the current value of the property to that value
## plus delta.

## The name of the property in the target node to interpolate.
var property : StringName
var delta : Variant

var trans_type := Tween.TransitionType.TRANS_LINEAR
var ease_type := Tween.EaseType.EASE_IN_OUT

var _start : Variant

func start():
	super.start()
	_start = node.get(property)

func act(delta : float) -> Status:
	var status = super.act(delta)
	var value = Tween.interpolate_value(_start, self.delta, percent, 1, trans_type, ease_type)
	node.set(property, value)
	return status

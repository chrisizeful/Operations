class_name NRelativeOperation
extends TimeOperation
## Interpolates a property on the target node over time.

## The value to interpolate to.
var value : Variant
## The name of the property that will be set to value.
var property : StringName
## Whether value is relative to the current value or not. For example,
## if Value is Vector2(200, 200) and a Node is positioned at (100, 100):
##	 Relative: ending position will be (300, 300)
##	 Not relative: ending position will be (200, 200)
var relative : bool
## Whether operations will take place in global or local space.
var global : bool

var trans_type := Tween.TransitionType.TRANS_LINEAR
var ease_type := Tween.EaseType.EASE_IN_OUT

var _start : Variant
var _goal : Variant

func start():
	_start = node.get(property)
	_goal = _delta_value()

func _delta_value() -> Variant:
	return -1

func act(delta : float) -> Status:
	var status = super.act(delta)
	var value = Tween.interpolate_value(_start, _goal, percent, 1, trans_type, ease_type)
	node.set(property, value)
	return status

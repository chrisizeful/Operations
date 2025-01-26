class_name NRotate2DOperation
extends NRelativeOperation
## Rotates the target 2D node.

## The target rotation.
var rotation_degrees : float:
	get:
		return value
	set(v):
		value = v

func start():
	property = "global_rotation_degrees" if global else "rotation_degrees"
	super.start()

func _delta_value() -> Variant:
	return rotation_degrees if relative else (rotation_degrees - _start)

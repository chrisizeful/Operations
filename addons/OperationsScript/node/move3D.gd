class_name NMove3DOperation
extends NRelativeOperation
## Moves the target 3D node to the provided position over time.

## The target 3D position.
var position : Vector3:
	get:
		return value
	set(v):
		value = v

func start():
	property = "global_position" if global else "position"
	super.start()

func _delta_value() -> Variant:
	return position if relative else (position - _start)

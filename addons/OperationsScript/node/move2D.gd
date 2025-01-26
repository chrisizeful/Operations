class_name NMove2DOperation
extends NRelativeOperation
## Moves the target 2D node to the provided position over time.

## The target 2D position.
var position : Vector2:
	get:
		return value
	set(v):
		value = v

func start():
	property = "global_position" if global else "position"
	super.start()

func _delta_value() -> Variant:
	return position if relative else (position - _start)

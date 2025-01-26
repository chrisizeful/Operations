class_name NScale3DOperation
extends NRelativeOperation
## Scales the target 3D node.

## The target scale.
var scale : Vector3:
	get:
		return value
	set(v):
		value = v

func start():
	property = "scale"
	super.start()

func _delta_value() -> Variant:
	return scale if relative else (scale - _start)

class_name NTransform2DOperation
extends NRelativeOperation

var transform : Transform2D:
	get:
		return value
	set(v):
		value = v

func start():
	property = "global_transform" if global else "transform"
	super.start()

func _delta_value() -> Variant:
	return (_start * transform) if relative else transform

func _interpolate() -> Variant:
	return _start.interpolate_with(_goal, _percent)

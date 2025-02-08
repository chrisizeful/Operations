class_name RotateDirOperation
extends TimeOperation
## A custom operation that lerps to a target angle over time.
## The target angle is calculated to face the specified Position.

var position : Vector3

var _start
var _rot_target

func start():
	super.start()
	var n : Node3D
	var delta = node.position - position
	_start = node.rotation.y
	_rot_target = atan2(delta.x, delta.y)

func act(delta : float):
	## Call super act beforehand instead of just returning it so the percent will be correct when used.
	var status = super.act(delta)
	var rotation_y = lerp_angle(_start, _rot_target, percent)
	node.rotation = Vector3(0, rotation_y, 0)
	return status

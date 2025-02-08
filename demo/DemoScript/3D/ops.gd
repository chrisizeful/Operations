class_name Ops
extends Object
## An example of creating a partial Op class for custom operations.

func rotate_dir(position : Vector3, duration : float) -> RotateDirOperation:
	var op := Pools.obtain(RotateDirOperation)
	op.position = position
	op.duration = duration
	return op

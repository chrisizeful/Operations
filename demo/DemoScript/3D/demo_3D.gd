class_name Demo3D
extends Node3D
## This is a 3D example of a semi-complicated operation.

var oper : Operator

func _ready():
	oper = Operator.new(get_tree())
	randomize()
	
	var files := Files.list_files("res://assets/kenney_toy-car-kit/vehicle/", "glb")
	for i in range(6):
		# Create a vehicle
		var vehicle := ResourceLoader.load(Files.random(files)).instantiate() as Node3D
		vehicle.position = Vector3(randf_range(-4, 4), 0, randf_range(-2, 2))
		vehicle.rotation_degrees = Vector3(0, randf_range(0, 360), 0)
		add_child(vehicle)
		
		# A sequence that bounces the scale up and down.
		var duration := 3.0
		var count := 4
		var scaler := Op.sequence()
		for j in range(count):
			var scale_down = Transform3D(Basis.IDENTITY.scaled(Vector3(.9, .9, .9)), Vector3.ZERO)
			scaler.children.append(Op.node_transform3D(scale_down, duration / count / 2, true, false, Tween.TransitionType.TRANS_BACK))
			var scale_up = Transform3D(Basis.IDENTITY.scaled(Vector3(1.1, 1.1, 1.1)), Vector3.ZERO)
			scaler.children.append(Op.node_transform3D(scale_up, duration / count / 2, true, false, Tween.TransitionType.TRANS_BACK))
		# This operation is repeated infinitely. An action is used to update the operations every repetition.
		var pos = Vector3(randf_range(-4.0, 4.0), 0, randf_range(-2.0, 2.0))
		var move := Op.node_move3D(pos, duration, false)
		var dir := Ops.rotate_dir(pos, .33)
		var action := func():
			var newPos = Vector3(randf_range(-4.0, 4.0), 0, randf_range(-2.0, 2.0))
			move.position = newPos
			dir.position = newPos
		oper.add(Op.repeat(
			Op.sequence(
				Op.action(action),
				Op.parallel(dir, move),
				scaler
			)).set_target(vehicle))

func _process(delta: float) -> void:
	oper.process()

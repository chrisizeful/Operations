class_name Demo3D
extends Node3D
## This is a 3D example of a semi-complicated operation.

var oper : Operator

func _ready():
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
		for j in range(count):
			pass 
		# This operation is repeated infinitely. An action is used to update the operations every repetition.

func _process(delta: float) -> void:
	pass

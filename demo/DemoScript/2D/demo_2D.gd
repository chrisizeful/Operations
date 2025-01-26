class_name Demo2D
extends Node2D
## This is a 2D example of a semi-complicated operation.

var oper : Operator

func _ready():
	randomize()
	
	for i in range(20):
		# Create a character
		var character := ResourceLoader.load("res://demo/DemoScript/2D/character2D.tscn").instantiate() as Character2D
		character.position = Vector2(randf_range(0, 1280), randf_range(0, 720))
		add_child(character)
		
		# This operation is repeated infinitely. An action is used in order to create a new operation
		# every repetition. Alternatively, the operation could've been stored outside the sequence and
		# just had its data changed in the action (see Demo3D for that).
		var duration := 3.0
		var parent := Op.sequence()
		var action := func():
			# Free the previous operation
			if parent.children.size() != 0:
				Pools.free_obj(parent.children[0])
			parent.children.clear()
			# Add a new move operation
			# TODO
		oper.add(Op.repeat(
			Op.sequence(
				Op.action(action),
				Op.parallel(
					Op.sequence(
						
					)
				)
			)).set_target(character))

func _process(delta: float) -> void:
	pass

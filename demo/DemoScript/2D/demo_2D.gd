class_name Demo2D
extends Node2D
## This is a 2D example of a semi-complicated operation.

var oper : Operator

func _ready():
	oper = Operator.new(get_tree())
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
			# Remove the previous operation
			parent.children.clear()
			# Add a new move operation, have to make sure set the target again
			parent.children.append(Op.node_move2D(
				Vector2(randf_range(0, 1280), randf_range(0, 720)),
				duration,
				false, false,
				Tween.TransitionType.TRANS_BACK,
				Tween.EaseType.EASE_IN).set_target(character))
		oper.add(Op.repeat(
			Op.sequence(
				Op.action(action),
				Op.parallel(
					Op.sequence(
						Op.node_scale2D(Vector2(.5, .5), duration / 2.0, false),
						Op.node_scale2D(Vector2(1.0, 1.0), duration / 2.0, false)),
					Op.node_rotate2D(90, duration),
					parent)
			)).set_target(character))

func _process(delta: float) -> void:
	oper.process()

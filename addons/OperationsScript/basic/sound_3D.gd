class_name Sound3DOperation
extends Operation
## Uses an <see cref="AudioStreamPlayer3D"/> to play a sound at a 3D <see cref="Position"/>.

## The 3D position to play the sound from.
var position : Vector3

func _instance() -> Node:
	var player := AudioStreamPlayer3D.new()
	player.position = position
	return player

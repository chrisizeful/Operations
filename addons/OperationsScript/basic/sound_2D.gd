class_name Sound2DOperation
extends Operation
## Uses an <see cref="AudioStreamPlayer2D"/> to play a sound at a 2D <see cref="Position"/>.

## The 2D position to play the sound from.
var position : Vector2

func _instance() -> Node:
	var player := AudioStreamPlayer2D.new()
	player.position = position
	return player

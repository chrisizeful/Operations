class_name SoundOperation
extends Operation
## Plays the provided sound at <see cref="Path"/> through the <see cref="Bus"/>. Uses an <see cref="AudioStreamPlayer"/> parented
## to the <see cref="Parent"/>, or the target node if null.

## The path to the audio file.
var path : String
## The name of the AudioBus to use, or null to use the default Master bus.
var bus : StringName
## The node that will parent the <see cref="AudioStreamPlayer"/>, or null to use the <see cref="Operation.Node"/>.
var parent : Node

func start():
	super.start()
	_create()

func _instance() -> Node:
	return AudioStreamPlayer.new()

func _create():
	var player := _instance()
	player.set("bus", "Master")
	player.set("stream", ResourceLoader.load(path))
	player.set("auto_player", true)
	var callable := func():
		player.queue_free()
		success()
	player.finished.connect(callable)
	player.process_mode = process_mode
	var parent = parent if parent else target
	parent.add_child(player)

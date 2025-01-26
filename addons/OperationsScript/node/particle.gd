class_name NParticleOperation
extends Operation
## Plays a particles scene at a specified position. The particles can be any one of the Godot particle nodes.

## The path of the particles scene.
var path : String
## The position to place the particles. Vector2 for 2D particles or Vector3 for 3D particles.
var position : Variant
## The node the particles will be a child of. If null, uses the target as the parent.
var parent : Node

func start():
	var particles := ResourceLoader.load(path).instantiate() as Node
	particles.set("position", position)
	particles.set("emitting", true)
	var n : Node
	n.connect("finished", func():
		success()
		particles.call_deferred("queue_free"))
	var parent = node if parent == null else parent
	parent.add_child(particles)

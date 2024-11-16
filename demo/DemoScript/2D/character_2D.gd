class_name Character2D
extends Sprite2D
## A basic character that consists of randomly chosen sprites.

@export
var face : Sprite2D
@export
var hand_left : Sprite2D
@export
var hand_right : Sprite2D

func _ready():
	var colors := ["blue", "green", "pink", "purple", "red", "yellow"]
	var color := Files.random(colors)
	
	var files := Files.list_files("res://assets/kenney_shape-characters//body/" + color + "/", "png")
	texture = ResourceLoader.load(Files.random(files))
	
	files = Files.list_files("res://assets/kenney_shape-characters/face/", "png")
	face.texture = ResourceLoader.load(Files.random(files))
	
	files = Files.list_files("res://assets/kenney_shape-characters/hand/" + color + "/", "png")
	hand_left.texture = ResourceLoader.load(Files.random(files))
	hand_right.texture = hand_left.texture

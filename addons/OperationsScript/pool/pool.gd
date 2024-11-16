class_name Pool
extends Object

var _type
var _max : int
var _peak : int
var _free_objects : Array

func _init(type, initial_capacity : int = 16, max : int = 9223372036854775807):
	_type = type
	_free_objects = []
	_free_objects.resize(initial_capacity)
	_max = max

func new_object() -> Object:
	return _type.new()

func obtain() -> Object:
	if _free_objects.size() == 0:
		return new_object()
	var obtained = _free_objects[0]
	_free_objects.remove_at(0)
	return obtained

func free_object(obj) -> void:
	if !obj:
		return
	if _free_objects.size() < _max:
		_free_objects.append(obj)
		_peak = max(_peak, _free_objects.size())
		_reset(obj)
	else:
		_reset(obj)
		_discard(obj)

func fill(size) -> void:
	for i in range(size):
		if _free_objects.size() < _max:
			_free_objects.append(new_object())
	_peak = max(_peak, _free_objects.size())

func _reset(obj) -> void:
	pass

func _discard(obj) -> void:
	pass

func _free_all(objects : Array) -> void:
	pass

func clear() -> void:
	for i in range(_free_objects.size()):
		var obj = _free_objects[i]
		_free_objects.remove_at(i)
		_discard(obj)

func get_free() -> int:
	return _free_objects.size()

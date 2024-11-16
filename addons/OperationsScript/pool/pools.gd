class_name Pools
extends Object

static var _pools : Dictionary

static func set_pool(type, pool : Pool) -> void:
	_pools[type] = pool

static func get_obj(type, max : int = 32) -> Pool:
	var pool := _pools.find_key(type)
	if pool:
		return pool
	pool = Pool.new(type, 4, max)
	_pools[type] = pool
	return pool

static func free_obj(obj):
	pass

static func obtain(type) -> Operation:
	return null

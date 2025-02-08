class_name Op
extends Object
## Provides convient methods for quickly creating pooled operations.

#region Basic

static func action(action : Callable) -> ActionOperation:
	var operation := Pools.obtain(ActionOperation)
	operation.action = action
	return operation

static func add(operator : Operator, operation : Operation) -> AddOperation:
	var add := Pools.obtain(AddOperation)
	add.operator = operator
	add.operation = operation
	return add

static func always_fail() -> AlwaysFailOperation:
	return Pools.obtain(AlwaysFailOperation)

static func always_running() -> AlwaysRunningOperation:
	return Pools.obtain(AlwaysRunningOperation)

static func always_succeed() -> AlwaysSucceedOperation:
	return Pools.obtain(AlwaysSucceedOperation)

static func audio_fade(out : bool, duration : float) -> AudioFadeOperation:
	var operation := Pools.obtain(AudioFadeOperation)
	operation.reverse = out
	operation.duration = duration
	return operation

static func defer(operation : Operation) -> DeferOperation:
	var op = Pools.obtain(DeferOperation)
	op.operation = operation
	return op

static func pressed(action : StringName) -> PressedOperation:
	var operation := Pools.obtain(PressedOperation)
	operation.action = action
	return operation

static func just_pressed(action : StringName) -> JustPressedOperation:
	var operation := Pools.obtain(JustPressedOperation)
	operation.action = action
	return operation

static func invert(child : Operation) -> InvertOperation:
	var operation := Pools.obtain(InvertOperation)
	operation.children.append(child)
	return operation

static func manual() -> ManualOperation:
	return Pools.obtain(ManualOperation)

static func process_mode(target : Operation, mode : Node.ProcessMode) -> ProcessModeOperation:
	var operation := Pools.obtain(ProcessModeOperation)
	operation.target_operation = target
	operation.mode = mode
	return operation

static func sound(path : String, bus := "Sound") -> SoundOperation:
	var operation := Pools.obtain(ProcessModeOperation)
	operation.path = path
	operation.bus = bus
	return operation

static func sound_2D(path : String, position : Vector2, bus := "Sound") -> Sound2DOperation:
	var operation := Pools.obtain(Sound2DOperation)
	operation.path = path
	operation.bus = bus
	return operation

static func sound_3D(path : String, position : Vector3, bus := "Sound") -> Sound3DOperation:
	var operation := Pools.obtain(Sound3DOperation)
	operation.path = path
	operation.bus = bus
	return operation

static func valid() -> ValidOperation:
	return Pools.obtain(ValidOperation)

static func wait(duration : float) -> TimeOperation:
	var operation := Pools.obtain(TimeOperation)
	operation.duration = duration
	return operation

#endregion

#region Node

static func animation(animation : StringName, custom_blend := -1.0, custom_speed := 1.0, from_end := false) -> NAnimationOperation:
	var operation := Pools.obtain(NAnimationOperation)
	operation.animation = animation
	operation.custom_blend = custom_blend
	operation.custom_speed = custom_speed
	operation.from_end = from_end
	return operation

static func animation_backwards(animation : StringName, custom_blend := -1.0, custom_speed := 1.0) -> NAnimationOperation:
	return animation(animation, custom_blend, custom_speed * -1, true)

static func free_node() -> NFreeOperation:
	return Pools.obtain(NFreeOperation)

static func node_method(method : Callable, from, to, duration : float, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NMethodOperation:
	var operation := Pools.obtain(NMethodOperation)
	operation.method = method
	operation.from = from
	operation.to = to
	operation.duration = duration
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_modulate(color : Color, duration : float, use_self := false, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NModulateOperation:
	var operation := Pools.obtain(NModulateOperation)
	operation.color = color
	operation.use_self = use_self
	operation.duration = duration
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func particles2D(path : String, position := Vector2()) -> NParticleOperation:
	var operation := Pools.obtain(NParticleOperation)
	operation.path = path
	operation.position = position
	return operation

static func particles3D(path : String, position := Vector3()) -> NParticleOperation:
	var operation := Pools.obtain(NParticleOperation)
	operation.path = path
	operation.position = position
	return operation

static func node_property(property : StringName, delta : Variant, duration : float, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NPropertyOperation:
	var operation := Pools.obtain(NModulateOperation)
	operation.property = property
	operation.delta = delta
	operation.duration = duration
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_ready() -> NReadyOperation:
	return Pools.obtain(NReadyOperation)

#endregion

#region Parent

static func guard_selector() -> GuardSelectorOperation:
	return Pools.obtain(GuardSelectorOperation)

static func indexed(starting : int = 0) -> IndexedOperation:
	var operation := Pools.obtain(IndexedOperation)
	operation.index = starting
	return operation

## FIXME No support for variadic functions in GDScript (https://github.com/godotengine/godot-proposals/issues/1034)
static func parallel(arg1 = null, arg2 = null, arg3 = null, arg4 = null, arg5 = null, arg6 = null, arg7 = null, arg8 = null, arg9 = null) -> ParallelOperation:
	var operation := Pools.obtain(ParallelOperation)
	if arg1:
		operation.children.append(arg1)
	if arg2:
		operation.children.append(arg2)
	if arg3:
		operation.children.append(arg3)
	if arg4:
		operation.children.append(arg4)
	if arg5:
		operation.children.append(arg5)
	if arg6:
		operation.children.append(arg6)
	if arg7:
		operation.children.append(arg7)
	if arg8:
		operation.children.append(arg8)
	if arg9:
		operation.children.append(arg9)
	return operation

static func random(probability : float) -> RandomOperation:
	var operation := Pools.obtain(RandomOperation)
	operation.probability = probability
	return operation

static func random_selector() -> RandomSelectorOperation:
	return Pools.obtain(RandomSelectorOperation)

static func random_sequence() -> RandomSequenceOperation:
	return Pools.obtain(RandomSequenceOperation)

static func repeat(child : Operation, limit := 0) -> RepeatOperation:
	var operation := Pools.obtain(RepeatOperation)
	operation.limit = limit
	operation.children.append(child)
	return operation

static func selector() -> SelectorOperation:
	return Pools.obtain(SelectorOperation)

## FIXME No support for variadic functions in GDScript (https://github.com/godotengine/godot-proposals/issues/1034)
static func sequence(arg1 = null, arg2 = null, arg3 = null, arg4 = null, arg5 = null, arg6 = null, arg7 = null, arg8 = null, arg9 = null) -> SequenceOperation:
	var operation := Pools.obtain(SequenceOperation)
	if arg1:
		operation.children.append(arg1)
	if arg2:
		operation.children.append(arg2)
	if arg3:
		operation.children.append(arg3)
	if arg4:
		operation.children.append(arg4)
	if arg5:
		operation.children.append(arg5)
	if arg6:
		operation.children.append(arg6)
	if arg7:
		operation.children.append(arg7)
	if arg8:
		operation.children.append(arg8)
	if arg9:
		operation.children.append(arg9)
	return operation

static func time_scale(child : Operation, scale : float) -> TimeScaleOperation:
	var operation := Pools.obtain(TimeScaleOperation)
	operation.scale = scale
	operation.children.append(child)
	return operation

static func until_fail(child : Operation) -> UntilFailOperation:
	var operation := Pools.obtain(UntilFailOperation)
	operation.children.append(child)
	return operation

static func until_succeed(child : Operation) -> UntilSucceedOperation:
	var operation := Pools.obtain(UntilSucceedOperation)
	operation.children.append(child)
	return operation

#endregion

#region Control



#endregion

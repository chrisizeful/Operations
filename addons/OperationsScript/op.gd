class_name Op
extends Object
## Provides convient methods for quickly creating pooled operations.

#region Basic

static func action(action : Callable) -> ActionOperation:
	var operation := ActionOperation.new()
	operation.action = action
	return operation

static func add(operator : Operator, operation : Operation) -> AddOperation:
	var add := AddOperation.new()
	add.operator = operator
	add.operation = operation
	return add

static func always_fail() -> AlwaysFailOperation:
	return AlwaysFailOperation.new()

static func always_running() -> AlwaysRunningOperation:
	return AlwaysRunningOperation.new()

static func always_succeed() -> AlwaysSucceedOperation:
	return AlwaysSucceedOperation.new()

static func audio_fade(out : bool, duration : float) -> AudioFadeOperation:
	var operation := AudioFadeOperation.new()
	operation.reverse = out
	operation.duration = duration
	return operation

static func defer(operation : Operation) -> DeferOperation:
	var op = DeferOperation.new()
	op.operation = operation
	return op

static func funco(sfunc : Callable) -> FuncOperation:
	var op = FuncOperation.new()
	op.sfunc = sfunc
	return op

static func pressed(action : StringName) -> PressedOperation:
	var operation := PressedOperation.new()
	operation.action = action
	return operation

static func just_pressed(action : StringName) -> JustPressedOperation:
	var operation := JustPressedOperation.new()
	operation.action = action
	return operation

static func invert(child : Operation) -> InvertOperation:
	var operation := InvertOperation.new()
	operation.children.append(child)
	return operation

static func manual() -> ManualOperation:
	return ManualOperation.new()

static func process_mode(target : Operation, mode : Node.ProcessMode) -> ProcessModeOperation:
	var operation := ProcessModeOperation.new()
	operation.target_operation = target
	operation.mode = mode
	return operation

static func sound(path : String, bus := "Sound") -> SoundOperation:
	var operation := SoundOperation.new()
	operation.path = path
	operation.bus = bus
	return operation

static func sound_2D(path : String, position : Vector2, bus := "Sound") -> Sound2DOperation:
	var operation := Sound2DOperation.new()
	operation.path = path
	operation.bus = bus
	return operation

static func sound_3D(path : String, position : Vector3, bus := "Sound") -> Sound3DOperation:
	var operation := Sound3DOperation.new()
	operation.path = path
	operation.bus = bus
	return operation

static func valid() -> ValidOperation:
	return ValidOperation.new()

static func wait(duration : float) -> TimeOperation:
	var operation := TimeOperation.new()
	operation.duration = duration
	return operation

#endregion

#region Node

static func animation(animation : StringName, custom_blend := -1.0, custom_speed := 1.0, from_end := false) -> NAnimationOperation:
	var operation := NAnimationOperation.new()
	operation.animation = animation
	operation.custom_blend = custom_blend
	operation.custom_speed = custom_speed
	operation.from_end = from_end
	return operation

static func animation_backwards(animation : StringName, custom_blend := -1.0, custom_speed := 1.0) -> NAnimationOperation:
	return animation(animation, custom_blend, custom_speed * -1, true)

static func free_node() -> NFreeOperation:
	return NFreeOperation.new()

static func node_method(method : Callable, from, to, duration : float, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NMethodOperation:
	var operation := NMethodOperation.new()
	operation.method = method
	operation.from = from
	operation.to = to
	operation.duration = duration
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_modulate(color : Color, duration : float, use_self := false, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NModulateOperation:
	var operation := NModulateOperation.new()
	operation.color = color
	operation.use_self = use_self
	operation.duration = duration
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_move2D(position : Vector2, duration : float, relative := true, global := false, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NMove2DOperation:
	var operation := NMove2DOperation.new()
	operation.position = position
	operation.duration = duration
	operation.relative = relative
	operation.global = global
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_move3D(position : Vector3, duration : float, relative := true, global := false, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NMove3DOperation:
	var operation := NMove3DOperation.new()
	operation.position = position
	operation.duration = duration
	operation.relative = relative
	operation.global = global
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func particles2D(path : String, position := Vector2()) -> NParticleOperation:
	var operation := NParticleOperation.new()
	operation.path = path
	operation.position = position
	return operation

static func particles3D(path : String, position := Vector3()) -> NParticleOperation:
	var operation := NParticleOperation.new()
	operation.path = path
	operation.position = position
	return operation

static func node_property(property : StringName, delta : Variant, duration : float, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NPropertyOperation:
	var operation := NPropertyOperation.new()
	operation.property = property
	operation.delta = delta
	operation.duration = duration
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_ready() -> NReadyOperation:
	return NReadyOperation.new()

static func node_relative(property : StringName, value : Variant, duration : float, relative := true, global := false, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NRelativeOperation:
	var operation := NRelativeOperation.new()
	operation.property = property
	operation.value = value
	operation.duration = duration
	operation.relative = relative
	operation.global = global
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_rotate2D(rotation_degrees : float, duration : float, relative := true, global := false, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NRotate2DOperation:
	var operation := NRotate2DOperation.new()
	operation.rotation_degrees = rotation_degrees
	operation.duration = duration
	operation.relative = relative
	operation.global = global
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_rotate3D(rotation_degrees : Vector3, duration : float, relative := true, global := false, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NRotate3DOperation:
	var operation := NRotate3DOperation.new()
	operation.rotation_degrees = rotation_degrees
	operation.duration = duration
	operation.relative = relative
	operation.global = global
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_scale2D(scale : Vector2, duration : float, relative := true, global := false, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NScale2DOperation:
	var operation := NScale2DOperation.new()
	operation.scale = scale
	operation.duration = duration
	operation.relative = relative
	operation.global = global
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_scale3D(scale : Vector3, duration : float, relative := true, global := false, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NScale3DOperation:
	var operation := NScale3DOperation.new()
	operation.scale = scale
	operation.duration = duration
	operation.relative = relative
	operation.global = global
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_set(property : StringName, value : Variant) -> NSetOperation:
	var operation := NSetOperation.new()
	operation.property = property
	operation.value = value
	return operation

static func node_signal(signal_name : StringName) -> NSignalOperation:
	var operation := NSignalOperation.new()
	operation.signal_name = signal_name
	return operation

static func node_transform2D(transform : Transform2D, duration : float, relative := true, global := false, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NTransform2DOperation:
	var operation := NTransform2DOperation.new()
	operation.transform = transform
	operation.duration = duration
	operation.relative = relative
	operation.global = global
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation
	
static func node_transform3D(transform : Transform3D, duration : float, relative := true, global := false, trans_type := Tween.TransitionType.TRANS_LINEAR, ease_type := Tween.EaseType.EASE_IN_OUT) -> NTransform3DOperation:
	var operation := NTransform3DOperation.new()
	operation.transform = transform
	operation.duration = duration
	operation.relative = relative
	operation.global = global
	operation.trans_type = trans_type
	operation.ease_type = ease_type
	return operation

static func node_visible(visible : bool) -> NVisibleOperation:
	var operation := NVisibleOperation.new()
	operation.visible = visible
	return operation

#endregion

#region Parent

static func guard_selector() -> GuardSelectorOperation:
	return GuardSelectorOperation.new()

static func indexed(starting : int = 0) -> IndexedOperation:
	var operation := IndexedOperation.new()
	operation.index = starting
	return operation

## FIXME No support for variadic functions in GDScript (https://github.com/godotengine/godot-proposals/issues/1034)
static func parallel(arg1 : Operation = null, arg2 : Operation = null, arg3 : Operation = null, arg4 : Operation = null, arg5 : Operation = null, arg6 : Operation = null, arg7 : Operation = null, arg8 : Operation = null, arg9 : Operation = null) -> ParallelOperation:
	var operation := ParallelOperation.new()
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
	var operation := RandomOperation.new()
	operation.probability = probability
	return operation

static func random_selector() -> RandomSelectorOperation:
	return RandomSelectorOperation.new()

static func random_sequence() -> RandomSequenceOperation:
	return RandomSequenceOperation.new()

static func repeat(child : Operation, limit := 0) -> RepeatOperation:
	var operation := RepeatOperation.new()
	operation.limit = limit
	operation.children.append(child)
	return operation

static func selector() -> SelectorOperation:
	return SelectorOperation.new()

## FIXME No support for variadic functions in GDScript (https://github.com/godotengine/godot-proposals/issues/1034)
static func sequence(arg1 : Operation = null, arg2 : Operation = null, arg3 : Operation = null, arg4 : Operation = null, arg5 : Operation = null, arg6 : Operation = null, arg7 : Operation = null, arg8 : Operation = null, arg9 : Operation = null) -> SequenceOperation:
	var operation := SequenceOperation.new()
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
	var operation := TimeScaleOperation.new()
	operation.scale = scale
	operation.children.append(child)
	return operation

static func until_fail(child : Operation) -> UntilFailOperation:
	var operation := UntilFailOperation.new()
	operation.children.append(child)
	return operation

static func until_succeed(child : Operation) -> UntilSucceedOperation:
	var operation := UntilSucceedOperation.new()
	operation.children.append(child)
	return operation

#endregion

#region Control

## Modulates the targets alpha from 0 to 1.
## FIXME No support for variadic functions in GDScript (https://github.com/godotengine/godot-proposals/issues/1034)
static func control_fade_in(arg1 : Control = null, arg2 : Control = null, arg3 : Control = null, arg4 : Control = null, arg5 : Control = null, arg6 : Control = null, arg7 : Control = null, arg8 : Control = null, arg9 : Control = null) -> Operation:
	var visible := parallel()
	var para = parallel()
	_control_fade_in(arg1, visible, para)
	_control_fade_in(arg2, visible, para)
	_control_fade_in(arg3, visible, para)
	_control_fade_in(arg4, visible, para)
	_control_fade_in(arg5, visible, para)
	_control_fade_in(arg6, visible, para)
	_control_fade_in(arg7, visible, para)
	_control_fade_in(arg8, visible, para)
	_control_fade_in(arg9, visible, para)
	return sequence(visible, para)

## Private function needed since control_fade_in doesn't used variadic parameters
static func _control_fade_in(control : Control, visible : Operation, para : Operation):
	if control:
		control.set_thread_safe("modulate", Color(1, 1, 1, 0))
		visible.children.append(node_visible(true).set_target(control))
		para.children.append(node_modulate(Color(1, 1, 1, 1), .15).set_target(control))

## Modulates the targets alpha from 1 to 0.
## FIXME No support for variadic functions in GDScript (https://github.com/godotengine/godot-proposals/issues/1034)
static func control_fade_out(arg1 : Control = null, arg2 : Control = null, arg3 : Control = null, arg4 : Control = null, arg5 : Control = null, arg6 : Control = null, arg7 : Control = null, arg8 : Control = null, arg9 : Control = null) -> Operation:
	var visible := parallel()
	var para = parallel()
	_control_fade_out(arg1, visible, para)
	_control_fade_out(arg2, visible, para)
	_control_fade_out(arg3, visible, para)
	_control_fade_out(arg4, visible, para)
	_control_fade_out(arg5, visible, para)
	_control_fade_out(arg6, visible, para)
	_control_fade_out(arg7, visible, para)
	_control_fade_out(arg8, visible, para)
	_control_fade_out(arg9, visible, para)
	return sequence(para, visible)

## Private function needed since control_fade_out doesn't used variadic parameters
static func _control_fade_out(control : Control, visible : Operation, para : Operation):
	if control:
		visible.children.append(node_visible(false).set_target(control))
		para.children.append(node_modulate(Color(1, 1, 1, 0), .15).set_target(control))

## Interpolates the targets scale from .5 to 1.
## FIXME No support for variadic functions in GDScript (https://github.com/godotengine/godot-proposals/issues/1034)
static func control_scale_in(arg1 : Control = null, arg2 : Control = null, arg3 : Control = null, arg4 : Control = null, arg5 : Control = null, arg6 : Control = null, arg7 : Control = null, arg8 : Control = null, arg9 : Control = null) -> Operation:
	var visible := parallel()
	var para = parallel()
	_control_scale_in(arg1, visible, para)
	_control_scale_in(arg2, visible, para)
	_control_scale_in(arg3, visible, para)
	_control_scale_in(arg4, visible, para)
	_control_scale_in(arg5, visible, para)
	_control_scale_in(arg6, visible, para)
	_control_scale_in(arg7, visible, para)
	_control_scale_in(arg8, visible, para)
	_control_scale_in(arg9, visible, para)
	return sequence(visible, para)

## Private function needed since control_scale_in doesn't used variadic parameters
static func _control_scale_in(control : Control, visible : Operation, para : Operation):
	if control:
		control.set_thread_safe("pivot_offset", control.size / 2)
		control.set_thread_safe("scale", Vector2(.5, .5))
		visible.children.append(node_visible(true).set_target(control))
		para.children.append(node_scale2D(Vector2(1.0, 1.0), .15, false, false).set_target(control))

## Interpolates the targets scale from 1 to .5.
## FIXME No support for variadic functions in GDScript (https://github.com/godotengine/godot-proposals/issues/1034)
static func control_scale_out(arg1 : Control = null, arg2 : Control = null, arg3 : Control = null, arg4 : Control = null, arg5 : Control = null, arg6 : Control = null, arg7 : Control = null, arg8 : Control = null, arg9 : Control = null) -> Operation:
	var visible := parallel()
	var para = parallel()
	_control_scale_out(arg1, visible, para)
	_control_scale_out(arg2, visible, para)
	_control_scale_out(arg3, visible, para)
	_control_scale_out(arg4, visible, para)
	_control_scale_out(arg5, visible, para)
	_control_scale_out(arg6, visible, para)
	_control_scale_out(arg7, visible, para)
	_control_scale_out(arg8, visible, para)
	_control_scale_out(arg9, visible, para)
	return sequence(para, visible)

## Private function needed since control_scale_out doesn't used variadic parameters
static func _control_scale_out(control : Control, visible : Operation, para : Operation):
	if control:
		visible.children.append(node_visible(false).set_target(control))
		para.children.append(node_scale2D(Vector2(.5, .5), .15, false, false).set_target(control))

## Modulates the targets alpha from 0 to alpha and interpolates the targets scale from .5 to 1.
## FIXME No support for variadic functions in GDScript (https://github.com/godotengine/godot-proposals/issues/1034)
static func control_scale_fade_in(alpha : float, arg1 : Control = null, arg2 : Control = null, arg3 : Control = null, arg4 : Control = null, arg5 : Control = null, arg6 : Control = null, arg7 : Control = null, arg8 : Control = null, arg9 : Control = null) -> Operation:
	var visible := parallel()
	var para = parallel()
	_control_scale_fade_in(alpha, arg1, visible, para)
	_control_scale_fade_in(alpha, arg2, visible, para)
	_control_scale_fade_in(alpha, arg3, visible, para)
	_control_scale_fade_in(alpha, arg4, visible, para)
	_control_scale_fade_in(alpha, arg5, visible, para)
	_control_scale_fade_in(alpha, arg6, visible, para)
	_control_scale_fade_in(alpha, arg7, visible, para)
	_control_scale_fade_in(alpha, arg8, visible, para)
	_control_scale_fade_in(alpha, arg9, visible, para)
	return sequence(visible, para)

## Private function needed since control_scale_fade_in doesn't used variadic parameters
static func _control_scale_fade_in(alpha : float, control : Control, visible : Operation, para : Operation):
	if control:
		control.set_thread_safe("pivot_offset", control.size / 2)
		control.set_thread_safe("scale", Vector2(.5, .5))
		control.set_thread_safe("modulate", Color(1, 1, 1, 0))
		visible.children.append(node_visible(true).set_target(control))
		para.children.append(node_scale2D(Vector2(1.0, 1.0), .15, false, false).set_target(control))
		para.children.append(node_modulate(Color(1, 1, 1, alpha), .15, false).set_target(control))

## Modulates the targets alpha from alpha to 0 and interpolates the targets scale from 1 to .5.
## FIXME No support for variadic functions in GDScript (https://github.com/godotengine/godot-proposals/issues/1034)
static func control_scale_fade_out(alpha : float, arg1 : Control = null, arg2 : Control = null, arg3 : Control = null, arg4 : Control = null, arg5 : Control = null, arg6 : Control = null, arg7 : Control = null, arg8 : Control = null, arg9 : Control = null) -> Operation:
	var visible := parallel()
	var para = parallel()
	_control_scale_fade_out(alpha, arg1, visible, para)
	_control_scale_fade_out(alpha, arg2, visible, para)
	_control_scale_fade_out(alpha, arg3, visible, para)
	_control_scale_fade_out(alpha, arg4, visible, para)
	_control_scale_fade_out(alpha, arg5, visible, para)
	_control_scale_fade_out(alpha, arg6, visible, para)
	_control_scale_fade_out(alpha, arg7, visible, para)
	_control_scale_fade_out(alpha, arg8, visible, para)
	_control_scale_fade_out(alpha, arg9, visible, para)
	return sequence(para, visible)

## Private function needed since control_scale_fade_out doesn't used variadic parameters
static func _control_scale_fade_out(alpha : float, control : Control, visible : Operation, para : Operation):
	if control:
		visible.children.append(node_visible(false).set_target(control))
		para.children.append(node_scale2D(Vector2(.5, .5), .15, false, false).set_target(control))
		para.children.append(node_modulate(Color(1, 1, 1, alpha), .15, false).set_target(control))

#endregion

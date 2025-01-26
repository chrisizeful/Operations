class_name NModulateOperation
extends TimeOperation
## Interpolates the color of a the target <see cref="CanvasItem"/> over time (i.e. an <see cref="Node2D"/> or <see cref="Control"/>).

## The target color.
var color : Color
## Whether to set the <see cref="CanvasItem.PropertyName.SelfModulate"/> or the <see cref="CanvasItem.PropertyName.Modulate"/>.
var use_self

var trans_type := Tween.TransitionType.TRANS_LINEAR
var ease_type := Tween.EaseType.EASE_IN_OUT

var _start : Color
var _end : Color

func start():
	super.start()
	_start = target.get("self_modulate" if use_self else "modulate")
	_end = Color()

func act(delta : float) -> Status:
	var status := super.act(delta)
	_end.r = Tween.interpolate_value(_start.r, color.r - _start.r, percent, 1, trans_type, ease_type);
	_end.g = Tween.interpolate_value(_start.g, color.g - _start.g, percent, 1, trans_type, ease_type);
	_end.b = Tween.interpolate_value(_start.b, color.b - _start.b, percent, 1, trans_type, ease_type);
	_end.a = Tween.interpolate_value(_start.a, color.a - _start.a, percent, 1, trans_type, ease_type);
	target.set("self_modulate" if use_self else "modulate", _end)
	return status

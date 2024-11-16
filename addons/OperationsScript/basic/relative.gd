class_name RelativeTimeOperation
extends TimeOperation
## Acts on relative time changes between frames instead of the overall complete percentage.

var _last_percent : float

func restart():
	super.restart()
	_last_percent = 0

func act(delta : float) -> Status:
	var status = super.act(delta)
	_act_relative(percent - _last_percent)
	_last_percent = percent
	return status

func _act_relative(percent_delta : float):
	pass

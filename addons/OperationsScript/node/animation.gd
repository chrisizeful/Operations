class_name NAnimationOperation
extends TimeOperation
## Plays the <see cref="Animation"/> from an <see cref="AnimationPlayer"/>. Succeeds
## when the animation finishes playing. Note that if the animation is set to loop the
## operation will succeed after the first full playing.

## The name of the animation to play.
var animation : StringName
var custom_blend := -1
var custom_speed := 1
var from_end

func start():
	super.start()
	var anim = target as AnimationPlayer
	anim.play(animation, custom_blend, custom_speed, from_end)
	duration = anim.current_animation_length

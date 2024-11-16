class_name AudioFadeOperation
extends TimeOperation
## Scales the <see cref="AudioStreamPlayer.VolumeDb"/> of an <see cref="AudioStreamPlayer"/> over time.

func act(delta : float) -> Status:
	var status := super.act(delta)
	target.set("volume_db", linear_to_db(percent))
	return status

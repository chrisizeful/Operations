using Godot;

namespace Operations;

/// <summary>
/// Scales the <see cref="AudioStreamPlayer.VolumeDb"/> of an <see cref="AudioStreamPlayer"/> over time.
/// </summary>
public class AudioFadeOperation : TimeOperation
{

    public override Status Act(double delta)
    {
        Status status = base.Act(delta);
        Node.Set(AudioStreamPlayer.PropertyName.VolumeDb, Mathf.LinearToDb((float) Percent));
        return status;
    }
}

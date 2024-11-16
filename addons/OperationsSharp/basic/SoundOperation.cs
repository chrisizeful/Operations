using Godot;

namespace Operations;

/// <summary>
/// Plays the provided sound at <see cref="Path"/> through the <see cref="Bus"/>. Uses an <see cref="AudioStreamPlayer"/> parented
/// to the <see cref="Parent"/>, or the target node if null.
/// </summary>
[Operation("Sound")]
public class SoundOperation : Operation
{

    /// <summary>
    /// The path to the audio file.
    /// </summary>
    public string Path;
    /// <summary>
    /// The name of the AudioBus to use, or null to use the default Master bus.
    /// </summary>
    public StringName Bus;
    /// <summary>
    /// The node that will parent the <see cref="AudioStreamPlayer"/>, or null to use the <see cref="Operation.Node"/>.
    /// </summary>
    public Node Parent;

    public override void Start()
    {
        base.Start();
        Create();
    }

    protected virtual Node Instance() => new AudioStreamPlayer();

    protected void Create()
    {
        Node player = Instance();
        player.Set(AudioStreamPlayer.PropertyName.Bus, Bus ?? "Master");
        player.Set(AudioStreamPlayer.PropertyName.Stream, ResourceLoader.Load<AudioStream>(Path));
        player.Set(AudioStreamPlayer.PropertyName.Autoplay, true);
        player.Connect(AudioStreamPlayer.SignalName.Finished, Callable.From(() => {
            player.QueueFree();
            Success();
        }));
        player.ProcessMode = ProcessMode;
        Node parent = Parent ?? Node;
        parent.AddChild(player);
    }
}

/// <summary>
/// Uses an <see cref="AudioStreamPlayer2D"/> to play a sound at a 2D <see cref="Position"/>.
/// </summary>
[Operation("Sound2D")]
public class Sound2DOperation : SoundOperation
{

    /// <summary>
    /// The 2D position to play the sound from.
    /// </summary>
    public Vector2 Position;

    protected override Node Instance()
    {
        return new AudioStreamPlayer2D()
        {
            Position = Position
        };
    }
}

/// <summary>
/// Uses an <see cref="AudioStreamPlayer3D"/> to play a sound at a 3D <see cref="Position"/>.
/// </summary>
[Operation("Sound3D")]
public class Sound3DOperation : SoundOperation
{

    /// <summary>
    /// The 3D position to play the sound from.
    /// </summary>
    public Vector3 Position;

    protected override Node Instance()
    {
        return new AudioStreamPlayer3D()
        {
            Position = Position
        };
    }
}

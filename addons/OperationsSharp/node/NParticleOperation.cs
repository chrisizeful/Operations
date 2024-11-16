using Godot;

namespace Operations;

/// <summary>
/// Plays a particles scene at a specified <see cref="Position"/>. The particles can be any one of the Godot particle nodes.
/// </summary>
[Operation("NodeParticles")]
public class NParticleOperation : Operation
{

    /// <summary>
    /// The path of the particles scene.
    /// </summary>
    public string Path;
    /// <summary>
    /// The position to place the particles. Vector2 for 2D particles or Vector3 for 3D particles.
    /// </summary>
    public Variant Position;
    /// <summary>
    /// The node the particles will be a child of. If null, uses the target as the parent.
    /// </summary>
    public Node Parent;

    public override void Start()
    {
        Node particles = ResourceLoader.Load<PackedScene>(Path).Instantiate();
        particles.Set(Node2D.PropertyName.Position, Position);
        particles.Set(CpuParticles2D.PropertyName.Emitting, true);
        // Connect via a Callable so that Node can of any particle type (i.e. CpuParticles3D, CpuParticles2D, etc)
        particles.Connect(CpuParticles2D.SignalName.Finished, Callable.From(() => {
            Success();
            particles.CallDeferred(Node.MethodName.QueueFree);
        }));
        Node parent = Parent ?? Node;
        parent.AddChild(particles);
    }
}

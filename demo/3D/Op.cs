using Godot;

namespace Operations;

/// <summary>
/// An example of creating a partial Op class for custom operations.
/// </summary>
public static partial class Op
{

    public static RotateDirOperation RotateDir(Vector3 position, float duration)
    {
        RotateDirOperation op = Pools.Obtain<RotateDirOperation>();
        op.Position = position;
        op.Duration = duration;
        return op;
    }
}
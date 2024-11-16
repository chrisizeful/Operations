using Godot;

namespace Operations;

/// <summary>
/// Prints the provided <see cref="What"/> to the console.
/// </summary>
[Operation("Print")]
public class PrintOperation : Operation
{

    /// <summary>
    /// What to print.
    /// </summary>
    public object[] What;

    public override Status Act(double delta)
    {
        GD.Print(What);
        return Status.Succeeded;
    }
}

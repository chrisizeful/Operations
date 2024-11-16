using Godot;

namespace Operations;

/// <summary>
/// Returns <see cref="Operation.Status.Succeeded"/> if the provided <see cref="Action"/> is just pressed.
/// </summary>
[Operation("JustPressed")]
public class JustPressedOperation : Operation
{

    /// <summary>
    /// The action to await input for.
    /// </summary>
    public StringName Action;

    public override Status Act(double delta)
    {
        if (Input.IsActionJustPressed(Action))
            return Status.Succeeded;
        return Status.Running;
    }
}

/// <summary>
/// Returns <see cref="Operation.Status.Succeeded"/> if the provided <see cref="Action"/> is pressed.
/// </summary>
[Operation("Pressed")]
public class PressedOperation : Operation
{

    /// <summary>
    /// The action to await input for.
    /// </summary>
    public StringName Action;

    public override Status Act(double delta)
    {
        if (Input.IsActionPressed(Action))
            return Status.Succeeded;
        return Status.Running;
    }
}

# Operations
Operations provides a quick and efficient way to animate and create complex behavior trees in the Godot game engine. A large collection of built-in operations are provided, with custom operations being very easy to make. See below for basic usage of the API, and the demos for more complex usages.

Example usage for the death animation of a 2D character may look like this:
```C#
using static Operations.Op;

Node character = ...;
Operation op =
    Sequence(
        NodeMove2D(new(0, 32), 2.0f),
        Parallel(
            NodeScale2D(new(2.0f, 2.0f), 1.0f),
            NodeRotate2D(90.0f, 1.0f)),
            NodeModulate(new(1, 0, 0, 0), 1.0f),
        Wait(1.0f),
        Free()
    ).SetTarget(character);
// In Process()...
Operator.Process(delta, op);
```

Example usage for the behavior tree of a basic cow may look like this:
```C#
using static Operations.Op;

Node cow = ...;
Operation op =
    Repeat(
        GuardSelector(
            Eat().SetGuard(GrassNearby()), // Custom operation and guard
            Die().SetGuard(HungerGuard(0), // Custom operation and guard
            Wander())                      // Custom operation
    ).SetTarget(cow);
// In Process()...
Operator.Process(delta, op);
```
### Custom operations
All operations extend from the Operation base class. A custom operation need only implement the Act() method. Although, many should also override the Restart(), Reset(), and End() methods. See the Operation class for all overridable methods, and built-in operations for common usage. For time based operations, extend TimeOperation or NRelativeOperation.

A basic example that prints a message and immediately returns a success status code:
```C#
using Operations;

public class CustomOperation : Operation
{

    public string Message;

    public override Status Act(double delta)
    {
        GD.Print(Message);
        return Status.SUCCEEDED;
    }

    public override void Reset()
    {
        base.Reset();
        Message = null;
    }
}
```

### Utility methods
All operations define a method in the Op class for easy static usage (see prior examples). Adding utility methods for your custom classes requires you create a partial Op class:
```C#
public static partial class Op
{

    public static CustomOperation Custom(string message)
    {
        CustomOperation operation = Pools.Obtain<CustomOperation>();
        operation.Message = message;
        return operation;
    }
}
```

### Targeting
Operations can target a specifc object. This allows a single operation to act on different nodes (or custom objects):
```C#
using static Operations.Op;

Node cow = ...;
Node human = ...;
Node cat = ...;
Operation op =
    Parallel(
        NodeRotate2D(-90, 2.0f).SetTarget(cow),
        NodeRotate2D(90, 2.0f).SetTarget(human),
        Free().SetTarget(cat)
    );
```

### Guards
Operations can optionally have a guard operation set. The actual usage of the guard is left up to the individual operation. For example, the GuardSelectorOperation runs the first child operation whose guard returns a successful status code. A guard is simply an Operatoin that can be evaluated as SUCCEEDED or FAILED in a single frame. Setting a guard is easy:
```C#
// Example custom guard
public class IsHitGuard : Operation
{
    public bool Hit;

    public override Status Act(double delta)
    {
        return Hit ? Status.SUCCEEDED : Status.FAILED;
    }
}

// Example usage of a guard
Node human = ...;
Operation op =
    Sequence(
        NodeRotate2D(-90, 2.0f).SetGuard(IsHitGuard()), // Custom operation guard
        NodeRotate2D(90, 2.0f).SetGuard(IsHitGuard()),  // Custom operation guard
    );
```

### Operator
In order for an operation to run it has to be added to an Operator. Operator is simply a class that is responsible for storing and processing operations. If an operation is added without a target set, the target will automatically be set to the SceneTree Root.
```C#
Operator oper = new(GetTree());
// In Ready()
Operation op = ...;
oper.Add(op);
// In Process()
oper.Process(delta);
```

Optionally, you can choose to run operations individually. Note that in this case you are responsible for freeing it when the operation completes.
```C#
Operation op = ...;
// In Process()
if (op != null && Operator.Process(delta, op))
{
    Pools.Free(op);
    op = null;
}
```

### Licensing
Operations is licensed under MIT - you are free to use it however you wish.

Do note, however, that all classes in Pools.cs are modified from [libGDX](https://github.com/libgdx/libgdx), which is licensed under the Apache License, Version 2.0. See the pools folder for more information. The demo project uses assets from Kenney's CC0 licensed [Shape Characters](https://kenney.nl/assets/shape-characters) and [Toy Car Kit](https://kenney.nl/assets/toy-car-kit) asset packs.

# Operations

Operations provides a quick and efficient way to programmatically create animations and complex behavior trees in the Godot game engine. A large collection of built-in operations are provided, with custom operations being very easy to make.

![Preview](https://i.imgur.com/HE5rFuH.gif)

### Examples
Example usage for the death animation of a 2D character may look like this:
```GDScript

```

Example usage for the behavior tree of a basic cow may look like this:
```GDScript

```

### Custom Operations
All operations extend from the Operation base class. A custom operation need only implement the Act() method. Although, many should also override the Restart(), Reset(), and End() methods. See the Operation class for all overridable methods, and built-in operations for common usage. For time based operations, extend TimeOperation or NRelativeOperation.

A basic example that prints a message and immediately returns a success status code:
```GDScript

```

### Utility Methods
All operations define a method in the Op class for easy static usage (see prior examples). Adding utility methods for your custom classes requires you create a partial Op class:
```GDScript

```

### Targeting
Operations can target a specifc object. Setting the target on an operation will set the target for all of its children, if the child does not already have a target. This allows a single operation to act on different nodes (or custom objects):
```GDScript

```

### Guards
Operations can optionally have a guard operation set. The actual usage of the guard is left up to the individual operation. For example, the GuardSelectorOperation runs the first child operation whose guard returns a successful status code. A guard is simply an Operation that can be evaluated as SUCCEEDED or FAILED in a single frame. Setting a guard is easy:
```GDScript

```

### Operator
In order for an operation to run it has to be added to an Operator. Operator is simply a class that is responsible for storing and processing operations. If an operation is added without a target set, the target will automatically be set to the SceneTree Root.
```GDScript

```

Optionally, you can choose to run operations individually in order to implement a custom solution. Note that in this case you are responsible for freeing it when the operation completes.
```GDScript

```

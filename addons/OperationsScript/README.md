![Preview](https://i.imgur.com/hMf5dGF.gif)

Operations provides a quick and efficient way to programmatically create animations and complex behavior trees in the Godot game engine. A large collection of built-in operations are provided, with custom operations being very easy to make.

### Examples
Example usage for the death animation of a 2D character may look like this:
```GDScript
var character = ...
var op =
    Op.sequence(
        Op.node_move2D(Vector2(0, 32), 2.0),
        Op.parallel(
            Op.node_scale2D(Vector2(2.0, 2.0), 1.0),
            Op.node_rotate2D(90.0, 1.0)),
        Op.node_modulate(Color(1, 0, 0, 0), 1.0),
        Op.wait(1.0),
        Op.free()
    ).set_target(character)
```

Example usage for the behavior tree of a basic cow may look like this:
```GDScript
var cow = ...
var op =
    Op.repeat(
        Op.guard_selector(
            eat().set_guard(grass_nearby())  # Custom operation and guard
            die().set_guard(hunger_guard(0)) # Custom operation and guard
            wander()                         # Custom operation
    )).set_target(cow).set_process_mode(Node.ProcessMode.PROCESS_MODE_ALWAYS)
```

### Custom Operations
All operations extend from the Operation base class. A custom operation need only implement the Act() method. Although, many should also override the Restart(), Reset(), and End() methods. See the Operation class for all overridable methods, and built-in operations for common usage. For time based operations, extend TimeOperation or NRelativeOperation.

A basic example that prints a message and immediately returns a success status code:
```GDScript
class_name CustomOperation
extends Operation

var message : string

func act(delta : float) -> Status:
    print(message)
    return Status.Succeeded

func reset():
    super.reset()
    message = null
```

### Utility Methods
All operations define a method in the Op class for easy static usage (see prior examples). Adding utility methods for your custom classes requires you create a new class:
```GDScript
class_name Ops
extends Object

static func custom(message : string) -> CustomOperation:
    var operation  = CustomOperation.new()
    operation.message = message
    return operation
```

### Targeting
Operations can target a specifc object. Setting the target on an operation will set the target for all of its children, if the child does not already have a target. This allows a single operation to act on different nodes (or custom objects):
```GDScript
var cow = ...
var human = ...
var cat = ...
var op =
    Op.parallel(
        Op.node_rotate2D(-90, 2.0) # This operation will target the cow, since no target was specified
        Op.node_rotate2D(90, 2.0).set_target(human),
        free().set_target(cat)
    ).set_target(cow)              # This will set the target for all children that don't have a target
```

### Guards
Operations can optionally have a guard operation set. The actual usage of the guard is left up to the individual operation. For example, the GuardSelectorOperation runs the first child operation whose guard returns a successful status code. A guard is simply an Operation that can be evaluated as SUCCEEDED or FAILED in a single frame. Setting a guard is easy:
```GDScript
# Example of a custom guard
class_name IsHitGuard
extends Operation

var hit : bool

func act(delta : float) -> Status:
    return Status.Succeeded if hit else Status.Failed

# Example usage of a custom guard
var human = ...
var op =
    Op.sequence(
        Op.node_rotate2D(-90, 2.0).set_guard(is_hit_guard()), # Custom operation guard
        Op.node_rotate2D(90, 2.0).set_guard(is_hit_guard())   # Custom operation guard
    ).set_target(human)
```

### Operator
In order for an operation to run it has to be added to an Operator. Operator is simply a class that is responsible for storing and processing operations. If an operation is added without a target set, the target will automatically be set to the SceneTree Root.
```GDScript
var oper = Operator.new(get_tree())
# In ready()
var op = ...
oper.add(op)
# In process()
oper.process()
```

Optionally, you can choose to run operations individually in order to implement a custom solution.
```GDScript
var op = ...
# In process()
if op != null and Operator.process_single(get_tree(), op):
    op = null
```

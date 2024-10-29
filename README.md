# Operations
Operations provides a quick and efficient way to animate and create complex behavior trees in the Godot game engine. A large collection of built-in operations are provided, with custom operations being very easy to make.

Example usage for the death animation of 2D character may look like this:
```C#
using static Operations;

Node target = ...;
Operation op =
    Sequence(
        NodeMove2D(new(0, 32), 2.0f),
        Parallel(
            NodeScale2D(new(2.0f, 2.0f), 1.0f),
            NodeRotate2D(90.0f, 1.0f)),
            NodeModulate(new(1, 0, 0, 0), 1.0f),
        Wait(1.0f),
        Free()
    ).SetTarget(target);
// In Process()...
Operator.Process(delta, op);
```

### Licensing
Operations is licensed under MIT - you are free to use it however you wish :). Do note, however: the Pools class, Pool class, and IPoolable interface are licensed under the Apache License, Version 2.0. The demo project uses assets from Kenney's CC0 licensed [Shape Characters](https://kenney.nl/assets/shape-characters) and [Toy Car Kit](https://kenney.nl/assets/toy-car-kit) asset packs.
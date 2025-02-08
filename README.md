# Operations
[![NuGet](https://img.shields.io/nuget/v/GodotOperations.svg)](https://www.nuget.org/packages/GodotOperations/)

Operations provides a quick and efficient way to programmatically create animations and complex behavior trees in the Godot game engine. A large collection of built-in operations are provided, with custom operations being very easy to make. It is available it both C# and GDSCript. For API usage and examples, see either the [GDScript README](https://github.com/chrisizeful/Operations/tree/main/addons/OperationsScript) or the [C# README](https://github.com/chrisizeful/Operations/tree/main/addons/OperationsSharp).

![Preview](https://i.imgur.com/HE5rFuH.gif)

### Quick Example
Example usage for the death animation of a 2D character may look like this:

#### C#
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
```

#### GDScript
```GDScript

```

### Installation
Copy either OperationsScript or OperationsSharp from the addons folder into the addons folder of your project. Read more about installing and enabling addons [here](https://docs.godotengine.org/en/stable/tutorials/plugins/editor/installing_plugins.html). The C# version is alternatively available as a Nuget package.

### Licensing
Operations is licensed under MIT - you are free to use it however you wish.

Do note, however, that all classes in Pools.cs are modified from [libGDX](https://github.com/libgdx/libgdx), which is licensed under the Apache License, Version 2.0. See the pools folder for more information. The demo project uses assets from Kenney's CC0 licensed [Shape Characters](https://kenney.nl/assets/shape-characters) and [Toy Car Kit](https://kenney.nl/assets/toy-car-kit) asset packs.

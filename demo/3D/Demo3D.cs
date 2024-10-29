using System.Collections.Generic;
using Godot;
using static Operations.Op;

namespace Operations;

public partial class Demo3D : Node3D
{

	Operator oper;

	public override void _Ready()
	{
		oper = new(GetTree());
		GD.Randomize();

		List<string> files = Files.ListFiles("res://assets/kenney_toy-car-kit/vehicle/", "glb");
		for (int i = 0; i < 5; i++)
		{
			Node3D vehicle = ResourceLoader.Load<PackedScene>(files.Random()).Instantiate<Node3D>();
			vehicle.Position = new(GD.RandRange(-4, 4), 0, GD.RandRange(-2, 2));
			vehicle.RotationDegrees = new(0, GD.RandRange(0, 360), 0);
			AddChild(vehicle);

			oper.Add(Sequence(
				NodeMove3D(new(GD.RandRange(-4, 4), 0, GD.RandRange(-2, 2)), 5.0f, false)
			).SetTarget(vehicle));
		}
	}

	public override void _Process(double delta)
	{
		oper.Process(delta);
	}
}

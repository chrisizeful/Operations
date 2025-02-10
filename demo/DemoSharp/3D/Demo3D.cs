using System.Collections.Generic;
using Godot;
using static Operations.Op;

namespace Operations;

/// <summary>
/// This is a 3D example of a semi-complicated operation.
/// </summary>
public partial class Demo3D : Node3D
{

	Operator oper;

	public override void _Ready()
	{
		oper = new(GetTree());
		GD.Randomize();

		List<string> files = Files.ListFiles("res://assets/kenney_toy-car-kit/vehicle/", "glb");
		for (int i = 0; i < 6; i++)
		{
			// Create a vehicle
			Node3D vehicle = ResourceLoader.Load<PackedScene>(files.Random()).Instantiate<Node3D>();
			vehicle.Position = new((float) GD.RandRange(-4.0f, 4.0f), 0, (float) GD.RandRange(-2.0f, 2.0f));
			vehicle.RotationDegrees = new(0, (float) GD.RandRange(0.0f, 360.0f), 0);
			AddChild(vehicle);

			// A sequence that bounces the scale up and down.
			float duration = 3.0f;
			int count = 4;
			Operation scaler = Sequence();
			for (int j = 0; j < count; j++)
			{
				scaler.Children.Add(NodeScale3D(new(-.1f, -.1f, -.1f), duration / count / 2, true, false, Tween.TransitionType.Back));
				scaler.Children.Add(NodeScale3D(new(.1f, .1f, .1f), duration / count / 2, true, false, Tween.TransitionType.Back));
			}
			// This operation is repeated infinitely. An action is used to update the operations every repetition.
			Vector3 pos = new((float) GD.RandRange(-4.0f, 4.0f), 0, (float) GD.RandRange(-2.0f, 2.0f));
			NMove3DOperation move = NodeMove3D(pos, duration, false);
			RotateDirOperation dir = RotateDir(pos, .33f);
			oper.Add(Repeat(
				Sequence(
					Action(() => {
						Vector3 newPos = new((float) GD.RandRange(-4.0f, 4.0f), 0, (float) GD.RandRange(-2.0f, 2.0f));
						move.Position = newPos;
						dir.Position = newPos;
					}),
					Parallel(scaler, dir, move)
				)).SetTarget(vehicle));
		}
	}

	public override void _Process(double delta) => oper.Process();
}

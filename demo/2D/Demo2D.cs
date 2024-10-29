using Godot;
using static Operations.Op;

namespace Operations;

public partial class Demo2D : Node2D
{

	Operator oper;

	public override void _Ready()
	{
		oper = new(GetTree());
		GD.Randomize();
		for (int i = 0; i < 20; i++)
		{
			Sprite2D character = ResourceLoader.Load<PackedScene>("res://demo/2D/Character2D.tscn").Instantiate<Sprite2D>();
			character.Position = new(GD.RandRange(0, 1280), GD.RandRange(0, 720));
			AddChild(character);

			oper.Add(Sequence(
				NodeMove2D(new(GD.RandRange(0, 1280), GD.RandRange(0, 720)), 5.0f, false)
			).SetTarget(character));
		}
	}

	public override void _Process(double delta)
	{
		oper.Process(delta);
	}
}

using Godot;
using static Operations.Op;

namespace Operations;

/// <summary>
/// This is a 2D example of a semi-complicated operation.
/// </summary>
public partial class Demo2D : Node2D
{

	Operator oper;

	public override void _Process(double delta) => oper.Process();
	public override void _Ready()
	{
		oper = new(GetTree());
		GD.Randomize();
		
		for (int i = 0; i < 20; i++)
		{
			// Create a character
			Sprite2D character = ResourceLoader.Load<PackedScene>("res://demo/2D/Character2D.tscn").Instantiate<Sprite2D>();
			character.Position = new((float) GD.RandRange(0.0f, 1280.0f), (float) GD.RandRange(0.0f, 720.0f));
			AddChild(character);

			// This operation is repeated infinitely. An action is used in order to create a new operation
			// every repetition. Alternatively, the operation could've been stored outside the sequence and
			// just had its data changed in the action (see Demo3D for that).
			float duration = 3.0f;
			Operation parent = Sequence();
			oper.Add(Repeat(
				Sequence(
					Action(() => {
						// Free the previous operation
						parent.Children.ForEach(Pools.Free);
						parent.Children.Clear();
						// Add a new move operation
						parent.Children.Add(NodeMove2D(
							new((float) GD.RandRange(0.0f, 1280.0f), (float) GD.RandRange(0.0f, 720.0f)),
							duration,
							false, false,
							Tween.TransitionType.Back,
							Tween.EaseType.In).SetTarget(character));
					}),
					Parallel(
						Sequence(
							NodeScale2D(new(.5f, .5f), duration / 2.0f, false),
							NodeScale2D(new(1.0f, 1.0f), duration / 2.0f, false)),
						NodeRotate2D(90, duration),
						parent)
				)).SetTarget(character));
		}
	}
}

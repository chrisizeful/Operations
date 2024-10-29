using Godot;

namespace Operations;

public partial class Character2D : Sprite2D
{

	[Export]
	public Sprite2D Face { get; private set; }
	[Export]
	public Sprite2D HandLeft { get; private set; }
	[Export]
	public Sprite2D HandRight { get; private set; }

	public override void _Ready()
	{
	}
}

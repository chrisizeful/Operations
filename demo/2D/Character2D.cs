using Godot;
using System.Collections.Generic;

namespace Operations;

/// <summary>
/// A basic character that consists of randomly chosen sprites.
/// </summary>
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
		string[] colors = new string[]
		{
			"blue", "green", "pink", "purple", "red", "yellow"
		};
		string color = colors[GD.RandRange(0, colors.Length - 1)];

		List<string> files = Files.ListFiles($"res://assets/kenney_shape-characters//body/{color}/", "png");
		Texture = ResourceLoader.Load<Texture2D>(files.Random());
		
		files = Files.ListFiles("res://assets/kenney_shape-characters/face/", "png");
		Face.Texture = ResourceLoader.Load<Texture2D>(files.Random());

		files = Files.ListFiles($"res://assets/kenney_shape-characters/hand/{color}/", "png");
		HandLeft.Texture = HandRight.Texture = ResourceLoader.Load<Texture2D>(files.Random());
	}
}

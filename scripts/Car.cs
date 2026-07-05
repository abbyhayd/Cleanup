using Godot;
using System;

public partial class Car : TrashDropper
{
	[Export] public Texture2D[] CarTextures { get; set; } = null!;
	public override void _Ready()
	{
		base._Ready();
		Speed = 250.0f;
		Direction = Vector2.Left;
		var sprite2D = GetNode<Sprite2D>("Sprite2D");
		if(Position.X < 0)
		{
			Direction = Vector2.Right;
			sprite2D.FlipH = true;
		}

		sprite2D.Texture = CarTextures[GD.Randi() % CarTextures.Length];
	}
	
}

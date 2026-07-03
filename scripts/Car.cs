using Godot;
using System;

public partial class Car : Node2D
{
	[Export] public float Speed { get; set; } = 200.0f;
	public Vector2 Direction { get; set; } = Vector2.Left;

	[Export] public Texture2D[] CarTextures { get; set; } = null!;

	public override void _Ready()
	{
		var sprite2D = GetNode<Sprite2D>("Sprite2D");
		if(Position.X < 0)
		{
			Direction = Vector2.Right;
			sprite2D.FlipH = true;
		}

		sprite2D.Texture = CarTextures[GD.Randi() % CarTextures.Length];
	}
	public override void _Process(double delta)
	{
		Position += Direction * Speed * (float)delta;
	}
	
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}

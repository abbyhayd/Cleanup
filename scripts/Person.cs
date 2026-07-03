using Godot;
using System;

public partial class Person : Node2D
{
	[Export] public float Speed { get; set; } = 200.0f;

	public Vector2 Direction { get; set; } = Vector2.Right;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if(Position.X > 0)
		{
			Direction = Vector2.Left;
			animatedSprite2D.FlipH = true;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += Direction * Speed * (float)delta;
	}
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}

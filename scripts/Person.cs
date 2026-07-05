using Godot;
using System;

public partial class Person : TrashDropper
{
	public override void _Ready()
	{
		Speed = 200.0f;
		Direction = Vector2.Right;

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if(Position.X > 0)
		{
			Direction = Vector2.Left;
			animatedSprite2D.FlipH = true;
		}
	}
}

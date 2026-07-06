using Godot;
using System;

public partial class SidewalkSweeper : TrashSweeper
{
	public override void _Ready()
	{
		base._Ready();
		Speed = 200.0f;
		Direction = Vector2.Right;
		ZIndex = 1;

		if(Position.X > 0)
		{
			var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			animatedSprite2D.FlipH = true;
			Direction = Vector2.Left;
			ZIndex = 4;
		}
	}

}

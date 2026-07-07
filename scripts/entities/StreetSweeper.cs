using Godot;
using System;

public partial class StreetSweeper : TrashSweeper
{
    public override void _Ready()
    {
        base._Ready();
		Speed = 250.0f;
		Direction = Vector2.Left;
		ZIndex = 2;

		if(Position.X < 0)
		{
			var sprite2D = GetNode<Sprite2D>("Sprite2D");
			sprite2D.FlipH = true;
			Direction = Vector2.Right;
			ZIndex = 3;
		}
    }
	public void OnSweeperCollision(Area2D area)
	{
		if(area is Car car)
		{
			car.QueueFree();
		}
	}
}

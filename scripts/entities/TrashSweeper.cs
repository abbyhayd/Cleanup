using Godot;
using System;

[GlobalClass]
public partial class TrashSweeper : Area2D
{
	[Export] public float Speed { get; set; } 
	public Vector2 Direction { get; set; }

	public override void _Process(double delta)
	{
		Position += Direction * Speed * (float)delta;
	}

	public void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}

	public void OnAreaEntered(Area2D area)
	{
		if(area is Trash trash)
		{
			trash.Cleaned();
		}
	}

}

using Godot;
using System;

public partial class Trash : Area2D
{
	private void OnInputEvent(Node viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			GD.Print("Trash clicked");
			QueueFree();
			GameManager.Score += 1;
		}
	}
}

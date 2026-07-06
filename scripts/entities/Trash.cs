using Godot;
using System;

public partial class Trash : Area2D
{
	[Export]public Texture2D[] trashTextures {get; set;}
	private Sprite2D sprite2D {get; set;}

    public override void _Ready()
    {
        base._Ready();
		sprite2D = GetNode<Sprite2D>("Sprite2D");
		int randomIndex = (int)(GD.Randi() % (uint)trashTextures.Length);
		sprite2D.Texture = trashTextures[randomIndex];
    }

	private void OnInputEvent(Node viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			Cleaned();
		}
	}

	public void Cleaned()
	{
		GameManager.Score += 1;
		QueueFree();
	}


}

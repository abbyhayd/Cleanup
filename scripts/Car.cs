using Godot;
using System;

public partial class Car : Node2D
{
	[Export] public float Speed { get; set; } = 250.0f;
	[Export] public PackedScene TrashScene { get; set; } = null!;

	[Export] public Texture2D[] CarTextures { get; set; } = null!;
	public Vector2 Direction { get; set; } = Vector2.Left;
	private bool onScreen = false;
	private bool droppedTrash = false;

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

		if(onScreen && !droppedTrash && GD.Randf() < Global.TRASH_SPAWN_CHANCE)
		{
			// GD.Print("Dropping trash from car");
			droppedTrash = true;
			Trash trash = TrashScene.Instantiate<Trash>();

			var trashSpawnMarker = GetNode<Marker2D>("TrashSpawnMarker");
			trash.GlobalPosition = trashSpawnMarker.GlobalPosition;

			var TrashContainer = GetParent().GetNode<Node2D>("../TrashContainer");
			TrashContainer.AddChild(trash);
		}
	}
	
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		onScreen = false;
		QueueFree();
	}
	private void OnVisibleOnScreenNotifier2DScreenEntered()
	{
		onScreen = true;
	}
}

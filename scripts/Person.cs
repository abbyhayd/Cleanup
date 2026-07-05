using Godot;
using System;

public partial class Person : Node2D
{
	[Export] public float Speed { get; set; } = 200.0f;
	[Export] public PackedScene TrashScene { get; set; } = null!;

	public Vector2 Direction { get; set; } = Vector2.Right;
	private bool onScreen = false; 
	private bool droppedTrash = false;


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

		if(onScreen && !droppedTrash && GD.Randf() < GameManager.TRASH_SPAWN_CHANCE)
		{
			// GD.Print("Dropping trash from person");
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

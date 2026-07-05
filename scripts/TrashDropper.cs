using Godot;
using System;

public partial class TrashDropper : Area2D
{
	[Export] public float Speed { get; set; } = 200.0f;
	[Export] public PackedScene TrashScene { get; set; } = null!;
	protected bool OnScreen { get; set; }= false; 
	protected bool DroppedTrash { get; set; }= false;
	public Vector2 Direction { get; set; }
	public bool InTrashSpawnArea { get; set; }= false;

	public override void _Process(double delta)
	{
		Position += Direction * Speed * (float)delta;

		if(OnScreen && !DroppedTrash && InTrashSpawnArea && GD.Randf() < GameManager.TRASH_SPAWN_CHANCE)
		{
			DroppedTrash = true;
			Trash trash = TrashScene.Instantiate<Trash>();

			var trashSpawnMarker = GetNode<Marker2D>("TrashSpawnMarker");
			trash.GlobalPosition = trashSpawnMarker.GlobalPosition;

			var TrashContainer = GetParent().GetNode<Node2D>("../TrashContainer");
			TrashContainer.AddChild(trash);
		}
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		OnScreen = false;
		QueueFree();
	}
	private void OnVisibleOnScreenNotifier2DScreenEntered()
	{
		OnScreen = true;
	}
}

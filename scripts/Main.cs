using Godot;
using System;

public partial class Main : Node2D
{

	[Export] public PackedScene PersonScene {get; set;} = null!;
	[Export] public PackedScene CarScene {get; set;} = null!;

	private void OnPersonSpawnTimerTimeout()
	{
		Person person = PersonScene.Instantiate<Person>();
		Node2D spawnPoints = GetNode<Node2D>("PersonSpawnMarkers");
		Marker2D spawnPoint = GetRandomChild(spawnPoints) as Marker2D ?? throw new Exception("No spawn points found.");
		person.Position = spawnPoint.Position;

		var SpawnedEntities = GetNode<Node2D>("SpawnedEntities");
		SpawnedEntities.AddChild(person);
	}

	private void OnCarSpawnTimerTimeout()
	{
		Car car = CarScene.Instantiate<Car>();
		Node2D spawnPoints = GetNode<Node2D>("CarSpawnMarkers");
		Marker2D spawnPoint = GetRandomChild(spawnPoints) as Marker2D ?? throw new Exception("No spawn points found.");
		car.Position = spawnPoint.Position;

		var SpawnedEntities = GetNode<Node2D>("SpawnedEntities");
		SpawnedEntities.AddChild(car);
	}


	private Node GetRandomChild(Node parent)
	{
		int childCount = parent.GetChildCount();
		if (childCount == 0)
		{
			return null!;
		}

		int randomIndex = (int)(GD.Randi() % childCount);
		return parent.GetChild(randomIndex);

	}
}

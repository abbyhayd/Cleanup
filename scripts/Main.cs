using Godot;
using System;

public partial class Main : Node2D
{

	[Export] public PackedScene PersonScene {get; set;} = null!;
	[Export] public PackedScene CarScene {get; set;} = null!;
	private Node2D _trashContainer;

	private Timer _personSpawnTimer;
	private Timer _carSpawnTimer;
	private CustomSignals _customSignals;

	public override void _Ready()
	{
		_personSpawnTimer = GetNode<Timer>("Timers/PersonSpawnTimer");
		_personSpawnTimer.WaitTime = GameManager.DEFAULT_PERSON_SPAWN_TIME;

		_carSpawnTimer = GetNode<Timer>("Timers/CarSpawnTimer");
		_carSpawnTimer.WaitTime = GameManager.DEFAULT_CAR_SPAWN_TIME;

		_trashContainer = GetNode<Node2D>("TrashContainer");

		_customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		_customSignals.Connect("RushHour", new Callable(this, nameof(RushHourStarted)));
		_customSignals.Connect("DayEnd", new Callable(this, nameof(DayEnd)));
		_customSignals.Connect("DayStart", new Callable(this, nameof(DayStart)));
	}

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

	public void RushHourStarted()
	{
		_personSpawnTimer.WaitTime = GameManager.DEFAULT_PERSON_SPAWN_TIME * GameManager.RUSHHOUR_PERSON_SPAWN_MULTIPLIER;
		_carSpawnTimer.WaitTime = GameManager.DEFAULT_CAR_SPAWN_TIME * GameManager.RUSHHOUR_CAR_SPAWN_MULTIPLIER;
	}
	public void DayEnd()
	{
		foreach(Node child in _trashContainer.GetChildren())
		{
			child.QueueFree();
		}
		_personSpawnTimer.Stop();
		_carSpawnTimer.Stop();
	}
	public void DayStart()
	{
		GD.Print("Day started in main");
		_personSpawnTimer.WaitTime = GameManager.DEFAULT_PERSON_SPAWN_TIME;
		_carSpawnTimer.WaitTime = GameManager.DEFAULT_CAR_SPAWN_TIME;
		_personSpawnTimer.Start();
		_carSpawnTimer.Start();
	}

	private void OnTrashSpawnAreaAreaEntered(Area2D area)
	{
		if(area is TrashDropper td)
		{
			td.InTrashSpawnArea = true;
		}
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

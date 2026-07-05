using Godot;
using System;

public partial class Main : Node2D
{

	[Export] public PackedScene PersonScene {get; set;} = null!;
	[Export] public PackedScene CarScene {get; set;} = null!;
	private Node2D _trashContainer;

	private Timer _personSpawnTimer;
	private Timer _carLeftSpawnTimer;
	private Timer _carRightSpawnTimer;
	private CustomSignals _customSignals;

	private Tween _cameraTween;
	private Camera2D _camera;


	//from 577, -769 to 577, 325

	public override void _Ready()
	{
		_camera = GetNode<Camera2D>("Camera2D");

		_personSpawnTimer = GetNode<Timer>("Timers/PersonSpawnTimer");
		_personSpawnTimer.WaitTime = GameManager.DEFAULT_PERSON_SPAWN_TIME;

		_carLeftSpawnTimer = GetNode<Timer>("Timers/CarLeftSpawnTimer");
		_carLeftSpawnTimer.WaitTime = GameManager.DEFAULT_CAR_SPAWN_TIME;
		_carRightSpawnTimer = GetNode<Timer>("Timers/CarRightSpawnTimer");
		_carRightSpawnTimer.WaitTime = GameManager.DEFAULT_CAR_SPAWN_TIME;

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
		if(spawnPoint.Name == "TopLeft" || spawnPoint.Name == "TopRight")
		{
			person.ZIndex = 1;
		}
		else
		{
			person.ZIndex = 4;
		}

		var SpawnedEntities = GetNode<Node2D>("SpawnedEntities");
		SpawnedEntities.AddChild(person);
	}

	private void OnCarLeftSpawnTimerTimeout()
	{
		Car car = CarScene.Instantiate<Car>();
		Marker2D leftSpawnPoint = GetNode<Marker2D>("CarSpawnMarkers/Left");
		car.Position = leftSpawnPoint.Position;
		car.ZIndex = 3;
		var SpawnedEntities = GetNode<Node2D>("SpawnedEntities");
		SpawnedEntities.AddChild(car);
	}
	private void OnCarRightSpawnTimerTimeout()
	{
		Car car = CarScene.Instantiate<Car>();
		Marker2D rightSpawnPoint = GetNode<Marker2D>("CarSpawnMarkers/Right");
		car.Position = rightSpawnPoint.Position;
		car.ZIndex = 2;
		var SpawnedEntities = GetNode<Node2D>("SpawnedEntities");
		SpawnedEntities.AddChild(car);
	}

	public void RushHourStarted()
	{
		_personSpawnTimer.WaitTime = GameManager.DEFAULT_PERSON_SPAWN_TIME * GameManager.RUSHHOUR_PERSON_SPAWN_MULTIPLIER;
		_carRightSpawnTimer.WaitTime = GameManager.DEFAULT_CAR_SPAWN_TIME * GameManager.RUSHHOUR_CAR_SPAWN_MULTIPLIER;
		_carLeftSpawnTimer.WaitTime = GameManager.DEFAULT_CAR_SPAWN_TIME * GameManager.RUSHHOUR_CAR_SPAWN_MULTIPLIER;
	}
	public void DayEnd()
	{
		foreach(Node child in _trashContainer.GetChildren())
		{
			child.QueueFree();
		}
		_personSpawnTimer.Stop();
		_carLeftSpawnTimer.Stop();
		_carRightSpawnTimer.Stop();
	}
	public void DayStart()
	{
		_personSpawnTimer.WaitTime = GameManager.DEFAULT_PERSON_SPAWN_TIME;
		_carLeftSpawnTimer.WaitTime = GameManager.DEFAULT_CAR_SPAWN_TIME;
		_carRightSpawnTimer.WaitTime = GameManager.DEFAULT_CAR_SPAWN_TIME;

		_personSpawnTimer.Start();
		_carLeftSpawnTimer.Start();
		_carRightSpawnTimer.Start();
	}

	private void OnTrashSpawnAreaAreaEntered(Area2D area)
	{
		if(area is TrashDropper td)
		{
			td.InTrashSpawnArea = true;
		}
	}
	private void OnTrashSpawnAreaAreaExited(Area2D area)
	{
		if(area is TrashDropper td)
		{
			td.InTrashSpawnArea = false;
		}
	}
	public async void OnStartButtonPressed()
	{
		_cameraTween = CreateTween();
		_cameraTween.SetTrans(Tween.TransitionType.Sine);
        _cameraTween.SetEase(Tween.EaseType.Out);

		_cameraTween.TweenProperty(_camera, "global_position", new Vector2(577, 325), 1.5f);
		await ToSignal(_cameraTween, Tween.SignalName.Finished);
		_customSignals.EmitSignal("DayStart");
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

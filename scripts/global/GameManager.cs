 using Godot;
using System;

[GlobalClass]
public partial class GameManager : Node
{
	public static int Score { get; set; } = 0;
	public static int DayScore {get; set;} = 0;
	public static int TotalScore {get; set;} = 0;
	public static int StreetSweeperCost {get; set;} = 10;
	public static int SidewalkSweeperCost {get; set;} = 10;
	public static float TRASH_SPAWN_CHANCE { get; set; } = 0.005f;
	public static float DEFAULT_PERSON_SPAWN_TIME { get; set; } = 1.5f;
	public static float DEFAULT_CAR_SPAWN_TIME { get; set; } = 2.0f;
	public static float RUSHHOUR_PERSON_SPAWN_MULTIPLIER { get; set; } = 0.6f;
	public static float RUSHHOUR_CAR_SPAWN_MULTIPLIER { get; set; } = 0.70f;
	
	private CustomSignals _customSignals;

    public override void _Ready()
    {
        base._Ready();
		_customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		_customSignals.Connect("DayStart", new Callable(this, nameof(ResetDay)));
    }

	public static void ResetDay()
	{
		Score = 0;
		DayScore = 0;
		StreetSweeperCost = 10;
		SidewalkSweeperCost = 10;
	}

}

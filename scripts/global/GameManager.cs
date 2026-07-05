 using Godot;
using System;

[GlobalClass]
public partial class GameManager : Node
{
	public static int Score { get; set; } = 0;
	public static float TRASH_SPAWN_CHANCE { get; set; } = 0.005f;
	public static float DEFAULT_PERSON_SPAWN_TIME { get; set; } = 1.5f;
	public static float DEFAULT_CAR_SPAWN_TIME { get; set; } = 1.5f;
	public static float RUSHHOUR_PERSON_SPAWN_MULTIPLIER { get; set; } = 0.5f;
	public static float RUSHHOUR_CAR_SPAWN_MULTIPLIER { get; set; } = 0.75f;
	
}

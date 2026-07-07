using Godot;
using System;

public partial class CustomSignals : Node
{
	[Signal] public delegate void RushHourEventHandler();
	[Signal] public delegate void DayEndEventHandler();
	[Signal] public delegate void DayStartEventHandler();
	[Signal] public delegate void StreetSweeperSpawnedEventHandler(Vector2 position);
	[Signal] public delegate void SidewalkSweeperSpawnedEventHandler(Vector2 position);
	[Signal] public delegate void TriggerClockPauseEventHandler();
}

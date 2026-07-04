 using Godot;
using System;

[GlobalClass]
public partial class Global : Node
{
	public static int Score { get; set; } = 0;
	public static float TRASH_SPAWN_CHANCE { get; set; } = 0.005f;
}

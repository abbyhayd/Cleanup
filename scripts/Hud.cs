using Godot;
using System;

public partial class Hud : Control
{
	private CustomSignals _customSignals;
	public override void _Ready()
	{
		_customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		_customSignals.Connect("RushHour", new Callable(this, nameof(RushHourStarted)));
	}
	public override void _Process(double delta)
	{
		var scoreLabel = GetNode<Label>("CanvasLayer/ScoreLabel");
		scoreLabel.Text = $"{GameManager.Score}";
	}

	public void RushHourStarted()
	{
		var rushHourLabel = GetNode<Label>("CanvasLayer/RushHourLabel");
		rushHourLabel.Visible = true;
	}
}

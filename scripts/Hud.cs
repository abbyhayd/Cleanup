using Godot;
using System;

public partial class Hud : Control
{
	private CustomSignals _customSignals;
	private Label _rushHourLabel;
	private Label _scoreLabel;
	private Panel _endOfDayPanel;
	public override void _Ready()
	{
		_customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		_customSignals.Connect("RushHour", new Callable(this, nameof(RushHourStarted)));
		_customSignals.Connect("DayEnd", new Callable(this, nameof(EndOfDay)));

		_rushHourLabel = GetNode<Label>("CanvasLayer/RushHourLabel");
		_scoreLabel = GetNode<Label>("CanvasLayer/ScoreLabel");
		_endOfDayPanel = GetNode<Panel>("CanvasLayer/EndOfDayPanel");
	}
	public override void _Process(double delta)
	{
		_scoreLabel.Text = $"{GameManager.Score}";
	}

	public void RushHourStarted()
	{
		_rushHourLabel.Visible = true;
	}

	public void EndOfDay()
	{
		_endOfDayPanel.Visible = true;
	}
}

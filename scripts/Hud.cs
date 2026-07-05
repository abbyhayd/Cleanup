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
		_customSignals.Connect("DayEnd", new Callable(this, nameof(DayEnd)));
		_customSignals.Connect("DayStart",new Callable(this, nameof(DayStart)));

		_rushHourLabel = GetNode<Label>("CanvasLayer/RushHourLabel");
		_scoreLabel = GetNode<Label>("CanvasLayer/ScoreLabel");
		_endOfDayPanel = GetNode<Panel>("CanvasLayer/EndOfDayPanel");
	}
	public override void _Process(double delta)
	{
		_scoreLabel.Text = $"{GameManager.Score}";
	}

	private void RushHourStarted()
	{
		_rushHourLabel.Visible = true;
	}

	private void DayEnd()
	{
		_endOfDayPanel.Visible = true;
		_rushHourLabel.Visible = false;
	}
	private void DayStart()
	{
		_endOfDayPanel.Visible = false;
	}
	public void OnNextDayButtonPressed()
	{
		_customSignals.EmitSignal("DayStart");
	}
}

using Godot;
using System;

public partial class EndOfDayPanel : Control
{
	private CustomSignals _customSignals;
	private Label _dayOfScoreLabel;
	private Label _totalScoreLabel;

	public override void _Ready()
	{
		_customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		_customSignals.Connect("DayEnd", new Callable(this, nameof(DayEnd)));
		_customSignals.Connect("DayStart",new Callable(this, nameof(DayStart)));

		_dayOfScoreLabel = GetNode<Label>("PanelTexture/DayOfScoreLabel");
		_totalScoreLabel = GetNode<Label>("PanelTexture/TotalScoreLabel");
	}
    public override void _Process(double delta)
    {
        base._Process(delta);
		if (Visible)
		{
			_dayOfScoreLabel.Text = $"Trash collected today: \n{GameManager.Score}";
			_totalScoreLabel.Text = $"Total score: {GameManager.TotalScore}";
		}
    }
	public void DayEnd()
	{
		Visible = true;
	}
	public void DayStart()
	{
		Visible = false;
	}
	

	public void OnNextDayButtonPressed()
	{
		_customSignals.EmitSignal("DayStart");
		
	}
}

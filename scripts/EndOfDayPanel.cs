using Godot;
using System;

public partial class EndOfDayPanel : Control
{
	private CustomSignals _customSignals;
	private Label _dayOfScoreLabel;
	private Label _totalScoreLabel;
	private AudioStreamPlayer _audio;


	public override void _Ready()
	{
		_customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		_customSignals.Connect("DayEnd", new Callable(this, nameof(DayEnd)));
		_customSignals.Connect("DayStart",new Callable(this, nameof(DayStart)));

		_dayOfScoreLabel = GetNode<Label>("PanelTexture/DayOfScoreLabel");
		_totalScoreLabel = GetNode<Label>("PanelTexture/TotalScoreLabel");
		_audio = GetNode<AudioStreamPlayer>("ButtonSelectSound");

	}
    public override void _Process(double delta)
    {
        base._Process(delta);
		if (Visible)
		{
			_dayOfScoreLabel.Text = $"Trash collected today: \n{GameManager.DayScore}";
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
		_audio.Play();

		_customSignals.EmitSignal("DayStart");
		
	}
}

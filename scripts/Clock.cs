using Godot;
using System;

[GlobalClass]
public partial class Clock : Control
{
	[Export] public Resource dateTime;
	[Export] public int ticksPerSecond = 120;
	[Export] public int TotalMinutesInDay {get; set;} = 300;
	private int _prevMinute;
	private int _curMinute;
	private CustomSignals _customSignals;
	private bool _rushHourStarted = false;

	public override void _Ready()
	{
		_customSignals = GetNode<CustomSignals>("/root/CustomSignals");
	}

    public override void _Process(double delta)
    {
        if(dateTime is DateTime dt)
		{	
			dt.IncreaseBySec((float)delta * ticksPerSecond);
			UpdateClock();
		}
    }

	private void UpdateClock()
	{
		var ClockLabel = GetNode<Label>("ClockControl/ClockLabel");
		var ClockArrow = GetNode<TextureRect>("ClockArrow");
		var anglePerMinute = 135.0f / TotalMinutesInDay;

		if(dateTime is DateTime dt)
		{
			ClockLabel.Text = $"{dt.Hours:D2}:{dt.Minutes:D2}";
			if(_prevMinute != dt.Minutes)
			{
				_curMinute = dt.Minutes;
				_prevMinute = _curMinute;
				
				ClockArrow.RotationDegrees += anglePerMinute;
				if(dt.Hours >= 4 && dt.Hours < 6 && !_rushHourStarted)
				{
					StartRushHour();
				}
				if(dt.Hours >= 6 && dt.Hours != 12)
				{
					EndDay();
				}
			}
		}
	}

	private void StartRushHour()
	{
		_rushHourStarted = true;
		GD.Print("Rush Hour Started");
		_customSignals.EmitSignal("RushHour");
	}
	private void EndDay()
	{
		GD.Print("End of Day");
	}
}

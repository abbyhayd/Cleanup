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
				GD.Print($"Clock Arrow Rotation: {ClockArrow.RotationDegrees}");
			}
			
		}
	}
}

 using Godot;
using System;

[Tool]
[GlobalClass]
public partial class DateTime : Resource
{
    [Export(PropertyHint.Range,"0, 59")] public int Seconds { get; set; } = 59;

    [Export(PropertyHint.Range,"0, 59")] public int Minutes { get; set; } = 0;

    [Export(PropertyHint.Range,"1, 12")] public int Hours { get; set; } = 5;
    public int TotalElapsedMinutes { get; set; } = 0;

    private float deltaTime = 0.0f;

    public DateTime(){}

    public void IncreaseBySec(float deltaSeconds)
    {
        deltaTime += deltaSeconds;
        if(deltaTime < 1)
        {
            return;
        }

        int deltaIntSecs = (int)deltaTime;

        deltaTime -= deltaIntSecs;
        Seconds += deltaIntSecs;
        Minutes += Seconds / 60;
        Hours += Minutes / 60;

        Seconds %= 60;
        Minutes %= 60;
        if(Hours % 12 == 0)
        {
            Hours = 12;
        }
        else
        {
            Hours %= 12;
        }

    }

}

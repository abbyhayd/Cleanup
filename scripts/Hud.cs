using Godot;
using System;

public partial class Hud : Control
{

	public override void _Process(double delta)
	{
		var scoreLabel = GetNode<Label>("CanvasLayer/ScoreLabel");
		scoreLabel.Text = $"{Global.Score}";
	}
}

 using Godot;
using System;
using System.ComponentModel;

public partial class Hud : Control
{
	private CustomSignals _customSignals;
	private Label _rushHourLabel;
	private Label _scoreLabel;
	private Label _streetCleanerCostLabel;
	private Label _sidewalkCleanerCostLabel;

	private TextureButton _topStreetSweeperButton;
	private TextureButton _bottomStreetSweeperButton;

	private TextureButton _topSidewalkSweeperButton;
	private TextureButton _bottomSidewalkSweeperButton;

	private AudioStreamPlayer _audio;

	public override void _Ready()
	{
		_customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		_customSignals.Connect("RushHour", new Callable(this, nameof(RushHourStarted)));
		_customSignals.Connect("DayEnd", new Callable(this, nameof(DayEnd)));

		_rushHourLabel = GetNode<Label>("RushHourLabel");
		_scoreLabel = GetNode<Label>("ScoreLabel");
		_streetCleanerCostLabel = GetNode<Label>("StreetCleanerCost");
		_sidewalkCleanerCostLabel = GetNode<Label>("SidewalkCleanerCost");
		_audio = GetNode<AudioStreamPlayer>("ButtonSelectSound");

		_topStreetSweeperButton = GetNode<TextureButton>("Tools/TopStreetSweeperButton");
		_bottomStreetSweeperButton = GetNode<TextureButton>("Tools/BottomStreetSweeperButton");
		_topSidewalkSweeperButton = GetNode<TextureButton>("Tools/TopSidewalkSweeperButton");
		_bottomSidewalkSweeperButton = GetNode<TextureButton>("Tools/BottomSidewalkSweeperButton");
	}
	public override void _Process(double delta)
	{
		_scoreLabel.Text = $"{GameManager.Score}";
		_streetCleanerCostLabel.Text = $"{GameManager.StreetSweeperCost}";
		_sidewalkCleanerCostLabel.Text = $"{GameManager.SidewalkSweeperCost}";
	}
	//================DAY CONTROL======================
	private void RushHourStarted()
	{
		_rushHourLabel.Visible = true;
	}

	private void DayEnd()
	{
		_rushHourLabel.Visible = false;
		SwitchSidewalkSweeperButtons(false);
		SwitchStreetSweeperButtons(false);
	}
	//==============================================

	//=================STREET SWEEP CONTROL====================

	private void SwitchStreetSweeperButtons(bool visible)
	{
		_topStreetSweeperButton.Visible = visible;
		_bottomStreetSweeperButton.Visible = visible;
	}
	public void OnStreetSweeperButton()
	{
		_audio.Play();

		if(GameManager.Score >= GameManager.StreetSweeperCost){
			SwitchStreetSweeperButtons(true);
			GameManager.Score -= GameManager.StreetSweeperCost;
		}
	}
	public void OnTopStreetSweeperButton()
	{	
		_audio.Play();

		GameManager.StreetSweeperCost += 3;
		_customSignals.EmitSignal("StreetSweeperSpawned", new Vector2(1315, 234));
		SwitchStreetSweeperButtons(false);
	}
	public void OnBottomStreetSweeperButton()
	{
		_audio.Play();

		GameManager.StreetSweeperCost += 3;
		_customSignals.EmitSignal("StreetSweeperSpawned", new Vector2(-163, 338));
		SwitchStreetSweeperButtons(false);

	}
	//==========================================

	//===============SIDEWALK SWEEPER CONTROL===============
	private void SwitchSidewalkSweeperButtons(bool visible)
	{
		_topSidewalkSweeperButton.Visible = visible;
		_bottomSidewalkSweeperButton.Visible = visible;
	}
	public void OnSidewalkSweeperButton()
	{
		_audio.Play();

		if(GameManager.Score >= GameManager.SidewalkSweeperCost){
			SwitchSidewalkSweeperButtons(true);
			GameManager.Score -= GameManager.SidewalkSweeperCost;
		}
	}
	public void OnTopSidewalkSweeperButton()
	{
		_audio.Play();

		GameManager.SidewalkSweeperCost += 2;
		_customSignals.EmitSignal("SidewalkSweeperSpawned", new Vector2(-72, 128));
		SwitchSidewalkSweeperButtons(false); 
	}
	public void OnBottomSidewalkSweeperButton()
	{
		_audio.Play();

		GameManager.SidewalkSweeperCost += 2;
		_customSignals.EmitSignal("SidewalkSweeperSpawned", new Vector2(1229, 434));
		SwitchSidewalkSweeperButtons(false); 
	}

	//======================================================
}

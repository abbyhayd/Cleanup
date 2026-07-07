using Godot;
using System;

public partial class Person : TrashDropper
{
	private Timer _footstepTimer;
	private AudioStreamPlayer2D _audioStreamPlayer;
	public override void _Ready()
	{
		base._Ready();
		Speed = 200.0f;
		Direction = Vector2.Right;


		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		string[] humanVariations = animatedSprite2D.SpriteFrames.GetAnimationNames();
		int randomIndex = (int)(GD.Randi() % (uint)humanVariations.Length);
		animatedSprite2D.Play(humanVariations[randomIndex]);

		if(Position.X > 0)
		{
			Direction = Vector2.Left;
			animatedSprite2D.FlipH = true;
		}
		_footstepTimer = GetNode<Timer>("FootstepTimer");
		_audioStreamPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
	}

    public override void _Process(double delta)
    {
        base._Process(delta);

		if (!_audioStreamPlayer.Playing && _footstepTimer.IsStopped())
		{
			_audioStreamPlayer.Play();
			_footstepTimer.Start();
		}
    }

}

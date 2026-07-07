using Godot;
using System;
using System.Threading.Tasks;

public partial class Trash : Area2D
{
	[Export]public Texture2D[] trashTextures {get; set;}
	private Sprite2D _sprite2D {get; set;}
	private AudioStreamPlayer2D _audio {get; set;}

    public override void _Ready()
    {
        base._Ready();
		_sprite2D = GetNode<Sprite2D>("Sprite2D");
		int randomIndex = (int)(GD.Randi() % (uint)trashTextures.Length);
		_sprite2D.Texture = trashTextures[randomIndex];

		_audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
    }

	private async void OnInputEvent(Node viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			await Cleaned();
		}
	}

	public async Task Cleaned()
	{
		GameManager.Score += 1;
		GameManager.TotalScore += 1;
		GameManager.DayScore +=1;

		Visible = false;
		_audio.Play();
		await ToSignal(_audio, AudioStreamPlayer2D.SignalName.Finished);

		QueueFree();
	}


}

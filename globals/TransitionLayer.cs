using Godot;
using System;

public partial class TransitionLayer : CanvasLayer
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public async void ChangeScene(String target)
	{
		AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("FadeToBlack");
        await ToSignal(animationPlayer, "animation_finished");
        GetTree().ChangeSceneToFile(target);
        animationPlayer.PlayBackwards("FadeToBlack");
    }
}

using Godot;
using System;

public partial class Inside : Level
{
    [Export]
    private PackedScene outsideLevelScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnExitGateAreaBodyEntered(PhysicsBody2D _body)
	{
        Tween tween = CreateTween();
        tween.TweenProperty(GetNode("Player"), "speed", 0, 0.5f);

		GetTree().ChangeSceneToPacked(outsideLevelScene);
    }
}

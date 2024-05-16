using Godot;
using System;

public partial class Outside : Level
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void OnGatePlayerEntered(PhysicsBody2D _body)
    {
        Tween tween = CreateTween();
        tween.TweenProperty(GetNode("Player"), "speed", 0, 0.5f);

		GetTree().ChangeSceneToFile("res://scenes/levels/inside.tscn");
    }

}

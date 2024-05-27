using Godot;
using System;

public partial class Inside : Level
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnExitGateAreaBodyEntered(PhysicsBody2D _body)
	{
        Tween tween = CreateTween();
        tween.TweenProperty(GetNode("Player"), "speed", 0, 0.5f);
        TransitionLayer transitionLayer = GetNode<TransitionLayer>("/root/TransitionLayer");
        transitionLayer.ChangeScene("res://scenes/levels/outside.tscn");
    }
}

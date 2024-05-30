using Godot;
using System;

public partial class Outside : Level
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void OnGatePlayerEntered(PhysicsBody2D _body)
    {
        Tween tween = CreateTween();
        tween.TweenProperty(GetNode("Player"), "speed", 0, 0.5f);
		TransitionLayer transitionLayer = GetNode<TransitionLayer>("/root/TransitionLayer");
		transitionLayer.ChangeScene("res://scenes/levels/inside.tscn");
    }

    public void OnHousePlayerEntered()
    {
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(GetNode("Player/Camera2D"), "zoom", new Vector2(1, 1), 1).SetTrans(Tween.TransitionType.Quad);
    }

    public void OnHousePlayerExited()
    {
        GetTree().CreateTween().TweenProperty(GetNode("Player/Camera2D"), "zoom", new Vector2(0.6f, 0.6f), 2);
    }

}

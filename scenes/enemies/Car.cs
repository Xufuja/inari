using Godot;
using System;

public partial class Car : PathFollow2D
{
	private bool playerNear = false;
	private Line2D line1;
	private Line2D line2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		line1 = GetNode<Line2D>("Turret/RayCast2D/Line2D");
        line2 = GetNode<Line2D>("Turret/RayCast2D2/Line2D");

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector2 playerPosition = GetNode<Globals>("/root/Globals").PlayerPosition;
        
		ProgressRatio += (float)(0.01f * delta);
		if (playerNear )
		{
			GetNode<Node2D>("Turret").LookAt(playerPosition);
		}
	}

	public void OnNoticeAreaBodyEntered(PhysicsBody2D body)
	{
		playerNear = true;
		GetNode<AnimationPlayer>("AnimationPlayer").Play("LaserLoad");
	}

	public void OnNoticeAreaBodyExited(PhysicsBody2D body)
	{
		playerNear = false;
	}

	public void Fire()
	{
		GetNode<Globals>("/root/Globals").Health -= 20;

    }
}

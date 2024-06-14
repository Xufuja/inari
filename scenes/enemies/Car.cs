using Godot;
using System;

public partial class Car : PathFollow2D
{
	private bool playerNear = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
	}

	public void OnNoticeAreaBodyExited(PhysicsBody2D body)
	{
		playerNear = false;
	}
}

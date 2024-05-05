using Godot;
using System;

public partial class Grenade : RigidBody2D
{
    public const int speed = 750;
    
	// Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
    }

	public void Explode()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Explosion");
	}
}

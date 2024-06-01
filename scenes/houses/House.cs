using Godot;
using System;

public partial class House : Area2D
{
    [Signal]
    public delegate void PlayerEnteredEventHandler(PhysicsBody2D body);

    [Signal]
    public delegate void PlayerExitedEventHandler(PhysicsBody2D body);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnBodyEntered(Node2D body)
	{
        EmitSignal(SignalName.PlayerEntered);
    }

	public void OnBodyExited(Node2D body)
	{
        EmitSignal(SignalName.PlayerExited);
    }
}

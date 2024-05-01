using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player : CharacterBody2D
{
	private bool canLaser = true;
	private bool canGrenade = true;

    [Signal]
    public delegate void LaserFiredEventHandler(Vector2 position, Vector2 direction);

    [Signal]
    public delegate void GrenadeFiredEventHandler(Vector2 position, Vector2 direction);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double _delta)
	{
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		Velocity = new Vector2(direction.X * 500, direction.Y * 500);
		MoveAndSlide();

        LookAt(GetGlobalMousePosition());

        Vector2 playerDirection = (GetGlobalMousePosition() - Position).Normalized();

        if (Input.IsActionPressed("primary action") && canLaser)
		{
            GetNode<GpuParticles2D>("GPUParticles2D").Emitting = true;

            Godot.Collections.Array<Marker2D> laserMarkers = new Godot.Collections.Array<Marker2D>(GetNode<Node2D>("LaserStartPositions").GetChildren().OfType<Marker2D>());
			Marker2D selectedLaser = laserMarkers[(int)(GD.Randi() % laserMarkers.Count)];

			canLaser = false;
			GetNode<Timer>("LaserTimer").Start();
            EmitSignal(SignalName.LaserFired, selectedLaser.GlobalPosition, playerDirection);
        }

        if (Input.IsActionPressed("secondary action") && canGrenade)
        {
            Vector2 position = GetNode<Node2D>("LaserStartPositions").GetChild<Marker2D>(0).GlobalPosition;

            canGrenade = false;
            GetNode<Timer>("GrenadeTimer").Start();
            EmitSignal(SignalName.GrenadeFired, position, playerDirection);
        }
    }

	public void OnLaserTimerTimeout()
	{
		canLaser = true;
	}

    public void OnGrenadeTimerTimeout()
    {
        canGrenade = true;
    }

}

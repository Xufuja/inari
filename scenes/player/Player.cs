using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player : CharacterBody2D
{
    [Export]
    private int maxSpeed = 500;
    private int speed;
    private bool canLaser = true;
	private bool canGrenade = true;

    [Signal]
    public delegate void LaserFiredEventHandler(Vector2 position, Vector2 direction);

    [Signal]
    public delegate void GrenadeFiredEventHandler(Vector2 position, Vector2 direction);

    [Signal]
    public delegate void UpdateStatsEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        speed = maxSpeed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double _delta)
	{
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		Velocity = new Vector2(direction.X * speed, direction.Y * speed);
		MoveAndSlide();

        LookAt(GetGlobalMousePosition());

        Vector2 playerDirection = (GetGlobalMousePosition() - Position).Normalized();

        if (Input.IsActionPressed("primary action") && canLaser && Globals.laserAmount > 0)
		{
            Globals.laserAmount -= 1;

            GetNode<GpuParticles2D>("GPUParticles2D").Emitting = true;

            Godot.Collections.Array<Marker2D> laserMarkers = new Godot.Collections.Array<Marker2D>(GetNode<Node2D>("LaserStartPositions").GetChildren().OfType<Marker2D>());
			Marker2D selectedLaser = laserMarkers[(int)(GD.Randi() % laserMarkers.Count)];

			canLaser = false;
			GetNode<Timer>("LaserTimer").Start();
            EmitSignal(SignalName.LaserFired, selectedLaser.GlobalPosition, playerDirection);
        }

        if (Input.IsActionPressed("secondary action") && canGrenade && Globals.grenadeAmount > 0)
        {
            Globals.grenadeAmount -= 1;

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

    public void AddItem(string type)
    {
        switch (type)
        {
            case "laser":
                {
                    Globals.laserAmount += 5;
                    break;
                }
            case "grenade":
                {
                    Globals.grenadeAmount += 5;
                    break;
                }
            case "health":
                {
                    GetNode<Sprite2D>("Sprite2D").Modulate = new Color(0.1f, 0.8f, 0.1f);
                    break;
                }
        }

        EmitSignal(SignalName.UpdateStats);
    }
}

using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private bool canLaser = true;
	private bool canGrenade = true;

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

		if (Input.IsActionPressed("primary action") && canLaser)
		{
			Console.WriteLine("Laser shot!");
			canLaser = false;

			GetNode<Timer>("LaserTimer").Start();
		}

        if (Input.IsActionPressed("secondary action") && canGrenade)
        {
            Console.WriteLine("Grenade shot!");
			canGrenade = false;

            GetNode<Timer>("GrenadeTimer").Start();
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

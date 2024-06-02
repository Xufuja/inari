using Godot;
using System;

public partial class Scout : CharacterBody2D
{
	private bool playerNearby = false;
    private bool canLaser = true;
	private bool alternate = true;

    [Signal]
    public delegate void LaserEventHandler(Vector2 position, Vector2 direction);

    public override void _Process(double delta)
	{
		Vector2 playerPosition = GetNode<Globals>("/root/Globals").PlayerPosition;

        LookAt(playerPosition);

		if (playerNearby)
		{
			if (canLaser)
			{
                Vector2 position = GetNode<Node2D>("LaserSpawnPositions").GetChild<Marker2D>(alternate ? 0 : 1).GlobalPosition;
                alternate = !alternate;
                Vector2 direction = (playerPosition - Position).Normalized();

                EmitSignal(SignalName.Laser, position, direction);
				canLaser = false;
				GetNode<Timer>("LaserCooldown").Start();
            }
         
		}
	}
	public void OnAttackAreaBodyEntered(PhysicsBody2D body)
	{
		playerNearby = true;
	}

	public void OnAttackAreaBodyExited(PhysicsBody2D body)
	{
		playerNearby = false;
	}

	public void OnLaserCooldownTimeout()
	{
		canLaser = true;
	}

	public void Hit()
	{
		Console.WriteLine("Scout Hit!");
	}
}

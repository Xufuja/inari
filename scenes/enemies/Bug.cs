using Godot;
using System;

public partial class Bug : CharacterBody2D
{
	private bool active = false;
	private int speed = 300;
	private bool vulnerable = true;
	private bool playerNear = false;

	public override void _Process(double delta)
	{
		Vector2 playerPosition = GetNode<Globals>("/root/Globals").PlayerPosition;
        Vector2 direction = (playerPosition - Position).Normalized();
		Velocity = new Vector2(direction.X * speed, direction.Y * speed);

		if (active)
		{
			MoveAndSlide();
			LookAt(playerPosition);
		}
    }

	public void OnAttackAreaBodyEntered(PhysicsBody2D body)
	{
		playerNear = true;
	}

	public void OnAttackAreaBodyExited(PhysicsBody2D body)
	{
		playerNear = false;
	}

	public void OnNoticeAreaBodyEntered(PhysicsBody2D body)
	{
		active = true;
	}

	public void OnNoticeAreaBodyExited(PhysicsBody2D body)
	{
		active = false;
	}
}

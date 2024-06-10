using Godot;
using System;

public partial class Drone : CharacterBody2D
{
    private bool active = false;
    private int speed = 400;
    private bool vulnerable = true;
    private int health = 50;

    public override void _Process(double _delta)
    {
        Vector2 playerPosition = GetNode<Globals>("/root/Globals").PlayerPosition;

        if (active)
        {
            LookAt(playerPosition);
            Vector2 direction = (playerPosition - Position).Normalized();
            Velocity = new Vector2(direction.X * speed, direction.Y * speed);
            MoveAndSlide();
        }
    }

    public async void Hit()
    {
        if (vulnerable)
        {
            vulnerable = false;
            GetNode<Timer>("HitTimer").Start();
            health -= 10;
        }

        if (health <= 0)
        {
            await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
            QueueFree();
        }
    }

    public void OnNoticeAreaBodyEntered(PhysicsBody2D body)
    {
        active = true;
    }

    public void OnHitTimerTimeout()
    {
        vulnerable = true;
    }
}

using Godot;
using System;

public partial class Drone : CharacterBody2D
{
    private bool active = false;
    private int speed = 400;
    private bool vulnerable = true;
    private int health = 50;

    public override void _Ready()
    {
        GetNode<Sprite2D>("Explosion").Hide();
        GetNode<Sprite2D>("DroneSprite").Show();
    }

    public override void _Process(double delta)
    {
        Vector2 playerPosition = GetNode<Globals>("/root/Globals").PlayerPosition;

        if (active)
        {
            LookAt(playerPosition);
            Vector2 direction = (playerPosition - Position).Normalized();
            Velocity = new Vector2(direction.X * speed, direction.Y * speed);
            KinematicCollision2D collision = MoveAndCollide(new Vector2((float)(Velocity.X * delta), (float)(Velocity.Y * delta)));

            if (collision != null)
            {
                GetNode<AnimationPlayer>("AnimationPlayer").Play("Explosion");
            }
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
            GetNode<AnimationPlayer>("AnimationPlayer").Play("Explosion");
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

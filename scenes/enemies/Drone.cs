using Godot;
using System;
using System.Reflection;

public partial class Drone : CharacterBody2D
{
    private bool active = false;
    private int maxSpeed = 600;
    private int speed = 0;
    private int speedMultiplier = 1;
    private bool vulnerable = true;
    private int health = 50;
    private bool explosionActive = false;

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
            Velocity = new Vector2(direction.X * speed * speedMultiplier, direction.Y * speed * speedMultiplier);
            KinematicCollision2D collision = MoveAndCollide(new Vector2((float)(Velocity.X * delta), (float)(Velocity.Y * delta)));

            if (collision != null)
            {
                GetNode<AnimationPlayer>("AnimationPlayer").Play("Explosion");
                explosionActive = true;
            }
        }

        if (explosionActive)
        {
            Godot.Collections.Array<Node> targets = (GetTree().GetNodesInGroup("Container") + GetTree().GetNodesInGroup("Entity"));

            foreach (Node target in targets)
            {
                bool inRange = ((Vector2)GetNode<Globals>("/root/Globals").GetProperty(target, "GlobalPosition")).DistanceTo(GlobalPosition) < 400;

                if (target.HasMethod("Hit") && inRange)
                {
                    Type type = target.GetType();

                    MethodInfo methodInfo = type.GetMethod("Hit");

                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(target, null);
                    }
                }
            }
        }
    }

    public async void Hit()
    {
        if (vulnerable)
        {
            vulnerable = false;
            GetNode<Timer>("HitTimer").Start();
            ((ShaderMaterial)GetNode<Sprite2D>("DroneSprite").Material).SetShaderParameter("progress", 1);
            health -= 10;
        }

        if (health <= 0)
        {
            GetNode<AnimationPlayer>("AnimationPlayer").Play("Explosion");
            explosionActive = true;
        }
    }

    public void OnNoticeAreaBodyEntered(PhysicsBody2D body)
    {
        active = true;
        GetTree().CreateTween().TweenProperty(this, "speed", maxSpeed, 6);
    }

    public void OnHitTimerTimeout()
    {
        vulnerable = true;
        ((ShaderMaterial)GetNode<Sprite2D>("DroneSprite").Material).SetShaderParameter("progress", 0);
    }

    public void StopMovement()
    {
        speedMultiplier = 0;
    }
}

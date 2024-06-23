using Godot;
using System;

public partial class Hunter : CharacterBody2D
{
    private bool active = false;
    private bool playerNear = false;
    private int speed = 200;
    private bool vulnerable = true;
    private int health = 100;

    public override void _Ready()
    {
        NavigationAgent2D navigationAgent2D = GetNode<NavigationAgent2D>("NavigationAgent2D");
        navigationAgent2D.PathDesiredDistance = 4.0f;
        navigationAgent2D.TargetDesiredDistance = 4.0f;
        navigationAgent2D.TargetPosition = GetNode<Globals>("/root/Globals").PlayerPosition;
     
    }

    public override void _PhysicsProcess(double delta)
    {
        if (active)
        {
            Vector2 nextPathPosition = GetNode<NavigationAgent2D>("NavigationAgent2D").GetNextPathPosition();
            Vector2 direction = (nextPathPosition - GlobalPosition).Normalized();
            Velocity = direction * speed;
            MoveAndSlide();
            float lookAngle = direction.Angle();
            Rotation = (float)(lookAngle + (Math.PI / 2));
        }
    }

    public void OnNoticeAreaBodyEntered(PhysicsBody2D body)
    {
        active = true;
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Walk");
    }

    public void OnNoticeAreaBodyExited(PhysicsBody2D body)
    {
        active = false;
    }

    public void OnNavigationTimerTimeout()
    {
        if (active)
        {
            GetNode<NavigationAgent2D>("NavigationAgent2D").TargetPosition = GetNode<Globals>("/root/Globals").PlayerPosition;
        }
    }

    public void OnAttackAreaBodyEntered(PhysicsBody2D body)
    {
        playerNear = true;
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Attack");
    }

    public void OnAttackAreaBodyExited(PhysicsBody2D body)
    {
        playerNear = false;
    }

    public void Attack()
    {
        if (playerNear)
        {
            GetNode<Globals>("/root/Globals").Health -= 20;
        }
    }

    public void OnHitTimerTimeout()
    {
        vulnerable = true;
    }

    public void Hit()
    {
        if (vulnerable)
        {
            health -= 10;
            GetNode<Timer>("Timers/HitTimer").Start();
        }
        if (health <= 0)
        {
            QueueFree();
        }
    }
}
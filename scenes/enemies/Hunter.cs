using Godot;
using System;

public partial class Hunter : CharacterBody2D
{
    private bool active = false;
    private int speed = 200;

    public override void _Ready()
    {
        GetNode<NavigationAgent2D>("NavigationAgent2D").TargetPosition = GetNode<Globals>("/root/Globals").PlayerPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (active)
        {
            Vector2 nextPathPosition = GetNode<NavigationAgent2D>("NavigationAgent2D").GetNextPathPosition();
            Vector2 direction = (nextPathPosition - GlobalPosition).Normalized();
            Velocity = direction * speed;
            MoveAndSlide();
            LookAt(GetNode<Globals>("/root/Globals").PlayerPosition);
        }
    }

    public void OnNoticeAreaBodyEntered(PhysicsBody2D body)
    {
        active = true;
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
}

using Godot;
using System;

public partial class Bug : CharacterBody2D
{
    private bool active = false;
    private int speed = 300;
    private bool vulnerable = true;
    private bool playerNear = false;
    private int health = 20;

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
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Attack");
    }

    public void OnAttackAreaBodyExited(PhysicsBody2D body)
    {
        playerNear = false;
    }

    public void OnNoticeAreaBodyEntered(PhysicsBody2D body)
    {
        active = true;
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Walk");
    }

    public void OnNoticeAreaBodyExited(PhysicsBody2D body)
    {
        active = false;
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Stop();
    }

    public void OnAnimatedSprite2DAnimationFinished()
    {
        if (playerNear)
        {
            GetNode<Globals>("/root/Globals").Health -= 10;
            GetNode<Timer>("Timers/AttackTimer").Start();
        }
    }

    public void OnAttackTimerTimeout()
    {
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Attack");
    }

    public void OnHitTimerTimeout()
    {
        vulnerable = true;
        ((ShaderMaterial)GetNode<AnimatedSprite2D>("AnimatedSprite2D").Material).SetShaderParameter("progress", 0);
    }

    public async void Hit()
    {
        if (vulnerable)
        {
            vulnerable = false;
            GetNode<Timer>("Timers/HitTimer").Start();
            health -= 10;
            ((ShaderMaterial)GetNode<AnimatedSprite2D>("AnimatedSprite2D").Material).SetShaderParameter("progress", 0.9f);
            GetNode<GpuParticles2D>("Particles/HitParticles").Emitting = true;
            GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
        }

        if (health <= 0)
        {
            await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
            QueueFree();
        }
    }
}

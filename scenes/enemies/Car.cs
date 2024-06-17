using Godot;
using System;

public partial class Car : PathFollow2D
{
	private bool playerNear = false;
	private Line2D line1;
	private Line2D line2;
	private Sprite2D gunFire1;
	private Sprite2D gunFire2;
    private RandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		line1 = GetNode<Line2D>("Turret/RayCast2D/Line2D");
        line2 = GetNode<Line2D>("Turret/RayCast2D2/Line2D");
        gunFire1 = GetNode<Sprite2D>("Turret/GunFire1");
        gunFire2 = GetNode<Sprite2D>("Turret/GunFire2");

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector2 playerPosition = GetNode<Globals>("/root/Globals").PlayerPosition;
        
		ProgressRatio += (float)(0.02f * delta);

		if (playerNear )
		{
			GetNode<Node2D>("Turret").LookAt(playerPosition);
		}
	}

	public void OnNoticeAreaBodyEntered(PhysicsBody2D body)
	{
		playerNear = true;
		GetNode<AnimationPlayer>("AnimationPlayer").Play("LaserLoad");
	}

	public async void OnNoticeAreaBodyExited(PhysicsBody2D body)
	{
		playerNear = false;
		GetNode<AnimationPlayer>("AnimationPlayer").Pause();

        Tween tween = GetTree().CreateTween();
        tween.SetParallel(true);
        tween.TweenProperty(line1, "width", 0, randomNumberGenerator.RandfRange(0.1f, 0.5f));
        tween.TweenProperty(line2, "width", 0, randomNumberGenerator.RandfRange(0.1f, 0.5f));

        await ToSignal(tween, "finished");
        GetNode<AnimationPlayer>("AnimationPlayer").Stop();
    }

	public void Fire()
	{
		GetNode<Globals>("/root/Globals").Health -= 20;
		gunFire1.Modulate = new Color(1, 1, 1, 1);
        gunFire2.Modulate = new Color(1, 1, 1, 1);

        Tween tween = GetTree().CreateTween();
        tween.SetParallel(true);
        tween.TweenProperty(gunFire1, "modulate:a", 0, randomNumberGenerator.RandfRange(0.1f, 0.5f));
        tween.TweenProperty(gunFire2, "modulate:a", 0, randomNumberGenerator.RandfRange(0.1f, 0.5f));
    }
}

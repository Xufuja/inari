using Godot;
using System;
using System.Reflection.Emit;

using static Godot.Mathf;

public partial class Level : Node2D
{
	private PackedScene laserScene = GD.Load<PackedScene>("res://scenes/projectiles/laser.tscn");
    private PackedScene grenadeScene = GD.Load<PackedScene>("res://scenes/projectiles/grenade.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
      
    }

   
    public void OnGatePlayerExited(PhysicsBody2D _body)
    {
        Console.WriteLine("Player Exited Gate!");
    }

    public void OnLaserFired(Vector2 position, Vector2 direction)
    {
        Laser laser = laserScene.Instantiate<Laser>();

        laser.Position = position;
        laser.RotationDegrees = RadToDeg(direction.Angle()) + 90;
        laser.Direction = direction;

        GetNode<Node2D>("Projectiles").AddChild(laser);
        GetNode<UI>("UI").UpdateLaserText();
    }

    public void OnGrenadeFired(Vector2 position, Vector2 direction)
    {
        RigidBody2D grenade = grenadeScene.Instantiate<RigidBody2D>();

        grenade.Position = position;
        grenade.LinearVelocity = new Vector2(direction.X * Grenade.speed, direction.Y * Grenade.speed);

        GetNode<Node2D>("Projectiles").AddChild(grenade);
        GetNode<UI>("UI").UpdateGrenadeText();
    }

    public void OnHousePlayerEntered()
    {
        Tween tween = GetTree().CreateTween();
        tween.SetParallel(true);
        tween.TweenProperty(GetNode("Player/Camera2D"), "zoom", new Vector2(1, 1), 1).SetTrans(Tween.TransitionType.Quad);
        //tween.TweenProperty(GetNode("Player"), "modulate:a", 0f, 2).From(0.5f);
    }

    public void OnHousePlayerExited()
    {
        GetTree().CreateTween().TweenProperty(GetNode("Player/Camera2D"), "zoom", new Vector2(0.6f, 0.6f), 2);
    }

    public void OnPlayerUpdateStats()
    {
        GetNode<UI>("UI").UpdateLaserText();
        GetNode<UI>("UI").UpdateGrenadeText();
    }
}

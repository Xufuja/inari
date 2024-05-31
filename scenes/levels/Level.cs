using Godot;
using System;
using System.Reflection.Emit;

using static Godot.Mathf;

public partial class Level : Node2D
{
	private PackedScene laserScene = GD.Load<PackedScene>("res://scenes/projectiles/laser.tscn");
    private PackedScene grenadeScene = GD.Load<PackedScene>("res://scenes/projectiles/grenade.tscn");
    private PackedScene itemScene = GD.Load<PackedScene>("res://scenes/items/item.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        foreach (ItemContainer container in GetTree().GetNodesInGroup("Container"))
        {
            container.Open += OnContainerOpened;
            //container.Connect(nameof(ItemContainer.Open), Callable.From(() => OnContainerOpened(Position, Position)));
        }
        foreach (Scout scout in GetTree().GetNodesInGroup("Scouts"))
        {
            scout.Laser += OnScoutLaser;
        }
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
        CreateLaser(position, direction);
    }

    public void CreateLaser(Vector2 position, Vector2 direction)
    {
        Laser laser = laserScene.Instantiate<Laser>();

        laser.Position = position;
        laser.RotationDegrees = RadToDeg(direction.Angle()) + 90;
        laser.Direction = direction;

        GetNode<Node2D>("Projectiles").AddChild(laser);
    }

    public void OnGrenadeFired(Vector2 position, Vector2 direction)
    {
        RigidBody2D grenade = grenadeScene.Instantiate<RigidBody2D>();

        grenade.Position = position;
        grenade.LinearVelocity = new Vector2(direction.X * Grenade.speed, direction.Y * Grenade.speed);

        GetNode<Node2D>("Projectiles").AddChild(grenade);
    }

    public void OnContainerOpened(Vector2 position, Vector2 direction)
    {
        Item item = itemScene.Instantiate<Item>();
        item.Position = position;
        item.Direction = direction;
        GetNode<Node2D>("Items").CallDeferred("add_child", item);
    }

    public void OnScoutLaser(Vector2 position, Vector2 direction)
    {
        CreateLaser(position, direction);
    }

}

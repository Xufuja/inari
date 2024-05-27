using Godot;
using System;
using System.Reflection;

public partial class Item : Area2D
{
    private int rotationSpeed = 4;
    private string[] availableOptions = { "laser", "laser", "laser", "laser", "grenade", "health" };
    private string type;
    private Vector2 direction = Vector2.Up;

    public Vector2 Direction { get => direction; set => direction = value; }

    private RandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();

    private int distance;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        distance = randomNumberGenerator.RandiRange(150, 250);
        type = availableOptions[(int)(GD.Randi() % availableOptions.Length)];

        switch (type)
        {
            case "laser":
                {
                    GetNode<Sprite2D>("Sprite2D").Modulate = new Color(0.1f, 0.2f, 0.8f);
                    break;
                }
            case "grenade":
                {
                    GetNode<Sprite2D>("Sprite2D").Modulate = new Color(0.8f, 0.2f, 0.1f);
                    break;
                }
            case "health":
                {
                    GetNode<Sprite2D>("Sprite2D").Modulate = new Color(0.1f, 0.8f, 0.1f);
                    break;
                }
        }

        Vector2 targetPosition = new Vector2(Position.X + (direction.X * distance), Position.Y + (direction.Y * distance));

        GetTree().CreateTween().TweenProperty(this, "position", targetPosition, 0.5f);

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Rotation += rotationSpeed * (float)delta;
    }

    public void OnBodyEntered(PhysicsBody2D _body)
    {
        Console.WriteLine(this.Name);
        Console.WriteLine(this.CollisionLayer);
        Console.WriteLine(this.CollisionMask);
        Console.WriteLine(_body.Name);
        Console.WriteLine(_body.CollisionLayer);
        Console.WriteLine(_body.CollisionMask);

        switch (type)
        {
            case "laser":
                {
                    GetNode<Globals>("/root/Globals").LaserAmount += 5;
                    break;
                }
            case "grenade":
                {
                    GetNode<Globals>("/root/Globals").GrenadeAmount += 1;
                    break;
                }
            case "health":
                {
                    GetNode<Globals>("/root/Globals").Health += 10;
                    break;
                }
        }

        QueueFree();
    }
}

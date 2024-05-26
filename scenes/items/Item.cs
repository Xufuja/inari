using Godot;
using System;
using System.Reflection;

public partial class Item : Area2D
{
    private int rotationSpeed = 4;
    private string[] availableOptions = { "laser", "laser", "laser", "laser", "grenade", "health" };
    private string type;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
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
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Rotation += rotationSpeed * (float)delta;
    }

    public void OnBodyEntered(PhysicsBody2D _body)
    {
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

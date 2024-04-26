using Godot;
using System;

public partial class Laser : Area2D
{
    [Export]
    private int speed = 1000;
    private Vector2 direction = Vector2.Up;

    public Vector2 Direction { get => direction; set => direction = value; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Position += new Vector2((float)(direction.X * speed * delta), (float)(direction.Y * speed * delta));
    }
}

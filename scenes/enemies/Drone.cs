using Godot;
using System;

public partial class Drone : CharacterBody2D
{
    public override void _Process(double _delta)
    {
        Vector2 direction = Vector2.Right;
        Velocity = new Vector2(direction.X * 100, direction.Y);
        MoveAndSlide();
    }

    public void Hit()
    {
        Console.WriteLine("Damage");
    }
}

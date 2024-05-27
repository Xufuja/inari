using Godot;
using System;

public partial class ItemContainer : StaticBody2D
{
	private Vector2 currentDirection;

    public Vector2 CurrentDirection { get => currentDirection; set => currentDirection = value; }

    [Signal]
    public delegate void OpenEventHandler(Vector2 position, Vector2 direction);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		currentDirection = Vector2.Down.Rotated(Rotation);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public virtual void Hit()
    {
        Console.WriteLine("Hit");
    }
}

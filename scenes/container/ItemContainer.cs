using Godot;
using System;

public partial class ItemContainer : StaticBody2D
{
	private Vector2 currentDirection;
    public Vector2 CurrentDirection { get => currentDirection; set => currentDirection = value; }

    public bool Opened { get => opened; set => opened = value; }
    private bool opened;

    [Signal]
    public delegate void OpenEventHandler(Vector2 position, Vector2 direction);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		currentDirection = Vector2.Down.Rotated(Rotation);
        opened = false;
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

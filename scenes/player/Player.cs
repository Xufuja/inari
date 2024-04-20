using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double _delta)
	{
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		Velocity = new Vector2(direction.X * 500, direction.Y * 500);
		MoveAndSlide();

		if (Input.IsActionPressed("primary action"))
		{
			Console.WriteLine("Primary");
		}

        if (Input.IsActionPressed("secondary action"))
        {
            Console.WriteLine("Secondary");
        }
    }
}

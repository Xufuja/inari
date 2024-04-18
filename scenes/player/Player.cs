using Godot;
using System;

public partial class Player : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		Position += new Vector2((float)(direction.X * 500 * delta), (float)(direction.Y * 500 * delta));

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

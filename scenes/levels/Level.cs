using Godot;
using System;

public partial class Level : Node2D
{
	private string[] testArray = { "Test", "Hello", "Stuff" };

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Sprite2D>("Logo").RotationDegrees = 90;

        Console.WriteLine(testArray[0]);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        GetNode<Sprite2D>("Logo").RotationDegrees += (float)(60 * delta);

		Vector2 position = GetNode<Logo>("Logo").Pos;

        if (position.X > 1000)
        {
            GetNode<Logo>("Logo").Pos = new Vector2(0, position.Y);
        }
    }
}

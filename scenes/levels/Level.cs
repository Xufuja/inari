using Godot;
using System;

public partial class Level : Node2D
{
	private string[] testArray = { "Test", "Hello", "Stuff" };

    public string[] TestArray { get => testArray; set => testArray = value; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		GetNode<Sprite2D>("Logo").RotationDegrees = 90;

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

		//Console.WriteLine(Input.IsActionPressed("left"));
    }

    public void TestFunction()
    {
        Console.WriteLine("Test Function!");
    }
}

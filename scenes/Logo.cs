using Godot;
using System;

public partial class Logo : Sprite2D
{
	private const int speed = 200;
	private Vector2 pos = Vector2.Zero;
	private int testScale = 1;

    public Vector2 Pos { get => pos; set => pos = value; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		pos = new Vector2(300, 200);
		testScale = 2;
		Position = pos;
		Scale = new Vector2(testScale, testScale);

		//int testRotation = 45;
		//RotationDegrees = testRotation;

		Console.WriteLine(string.Join(", ", GetNode<Level>("..").TestArray));
		GetNode<Level>("..").TestFunction();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		pos = new Vector2((float)(pos.X + speed * delta), pos.Y);
		Position = pos;
		//testScale += 1;
		//Scale = new Vector2(testScale, testScale);
	}

}

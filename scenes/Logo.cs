using Godot;
using System;

public partial class Logo : Sprite2D
{
	private const int speed = 10;
	private Vector2 position = Vector2.Zero;
	private int testScale = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		position = new Vector2(300, 200);
		testScale = 2;
		Position = position;
		Scale = new Vector2(testScale, testScale);

		int testRotation = 45;
		RotationDegrees = testRotation;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		position = new Vector2(position.X + speed, position.Y);
		Position = position;
		testScale += 1;
		Scale = new Vector2(testScale, testScale);
	}
}

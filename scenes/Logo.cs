using Godot;
using System;

public partial class Logo : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = new Vector2(100, 200);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = new Vector2(Position.X + 10, Position.Y);
	}
}

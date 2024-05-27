using Godot;
using System;

public partial class BoxContainer : ItemContainer
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

    }

    public override void Hit()
    {
		GetNode<Sprite2D>("LidSprite").Hide();
		Node2D spawnPositions = GetNode<Node2D>("SpawnPositions");
		Vector2 position = spawnPositions.GetChild<Marker2D>((int)(GD.Randi() % spawnPositions.GetChildCount())).GlobalPosition;
        EmitSignal(SignalName.Open, position, CurrentDirection);
    }
}

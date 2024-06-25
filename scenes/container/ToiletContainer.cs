using Godot;
using System;

public partial class ToiletContainer : ItemContainer
{
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void Hit()
    {
        if (!Opened)
        {
            GetNode<Sprite2D>("LidSprite").Hide();
            GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
            Vector2 position = GetNode<Node2D>("SpawnPositions/Marker2D").GlobalPosition;
            EmitSignal(SignalName.Open, position, CurrentDirection);

            Opened = true;
        }
    }
}

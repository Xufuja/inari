using Godot;
using System;

public partial class UI : CanvasLayer
{
	private Label laserLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        laserLabel = GetNode<Label>("LaserCounter/VBoxContainer/Label");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateLaserText()
	{
		laserLabel.Text = Globals.laserAmount.ToString(); 
	}
}

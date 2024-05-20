using Godot;
using System;

public partial class UI : CanvasLayer
{
	private Label laserLabel;
    private Label grenadeLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        laserLabel = GetNode<Label>("LaserCounter/VBoxContainer/Label");
        grenadeLabel = GetNode<Label>("GrenadeCounter/VBoxContainer/Label");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        UpdateLaserText();
        UpdateGrenadeText();
	}

	public void UpdateLaserText()
	{
		laserLabel.Text = Globals.laserAmount.ToString(); 
	}

    public void UpdateGrenadeText()
    {
        grenadeLabel.Text = Globals.grenadeAmount.ToString();
    }
}

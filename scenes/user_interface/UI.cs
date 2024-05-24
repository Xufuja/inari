using Godot;
using System;

public partial class UI : CanvasLayer
{
	private Label laserLabel;
    private Label grenadeLabel;
    private TextureRect laserIcon;
    private TextureRect grenadeIcon;

    private Color green = new Color("6BBFA3");
    private Color red = new Color(0.9f, 0, 0, 1);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        laserLabel = GetNode<Label>("LaserCounter/VBoxContainer/Label");
        grenadeLabel = GetNode<Label>("GrenadeCounter/VBoxContainer/Label");
        laserIcon = GetNode<TextureRect>("LaserCounter/VBoxContainer/TextureRect");
        grenadeIcon = GetNode<TextureRect>("GrenadeCounter/VBoxContainer/TextureRect");
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
        UpdateColor(Globals.laserAmount, laserIcon, laserLabel);
	}

    public void UpdateGrenadeText()
    {
        grenadeLabel.Text = Globals.grenadeAmount.ToString();
        UpdateColor(Globals.grenadeAmount, grenadeIcon, grenadeLabel);
    }

    public void UpdateColor(int amount, TextureRect icon, Label label)
    {
        if (amount <= 0)
        {
            label.Modulate = red;
            icon.Modulate = red;
        } else
        {
            label.Modulate = green;
            icon.Modulate = green;
        }
    }
}

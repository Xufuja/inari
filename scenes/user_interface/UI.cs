using Godot;
using System;

public partial class UI : CanvasLayer
{
	private Label laserLabel;
    private Label grenadeLabel;
    private TextureRect laserIcon;
    private TextureRect grenadeIcon;
    private TextureProgressBar healthBar;

    private Color green = new Color("6BBFA3");
    private Color red = new Color(0.9f, 0, 0, 1);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GetNode<Globals>("/root/Globals").HealthChange += UpdateHealthText;

        laserLabel = GetNode<Label>("LaserCounter/VBoxContainer/Label");
        grenadeLabel = GetNode<Label>("GrenadeCounter/VBoxContainer/Label");
        laserIcon = GetNode<TextureRect>("LaserCounter/VBoxContainer/TextureRect");
        grenadeIcon = GetNode<TextureRect>("GrenadeCounter/VBoxContainer/TextureRect");
        healthBar = GetNode<TextureProgressBar>("MarginContainer/TextureProgressBar");

        UpdateLaserText();
        UpdateGrenadeText();
        UpdateHealthText();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateLaserText()
	{
		laserLabel.Text = GetNode<Globals>("/root/Globals").LaserAmount.ToString();
        UpdateColor(GetNode<Globals>("/root/Globals").LaserAmount, laserIcon, laserLabel);
	}

    public void UpdateGrenadeText()
    {
        grenadeLabel.Text = GetNode<Globals>("/root/Globals").GrenadeAmount.ToString();
        UpdateColor(GetNode<Globals>("/root/Globals").GrenadeAmount, grenadeIcon, grenadeLabel);
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

    public void UpdateHealthText()
    {
        healthBar.Value = GetNode<Globals>("/root/Globals").Health;
    }
}

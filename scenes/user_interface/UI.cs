using Godot;
using System;

public partial class UI : CanvasLayer
{
    private Globals globals;
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
        globals = GetNode<Globals>("/root/Globals");

        //The "new" way of connecting signals is bugged and will return "Cannot access a disposed object" errors: //https://github.com/godotengine/godot/issues/70414
        if (Globals.CLASSIC_SIGNALS)
        {
            globals.Connect(nameof(globals.HealthChange), Callable.From(UpdateStatsText));
            globals.Connect(nameof(globals.LaserAmountChange), Callable.From(UpdateStatsText));
            globals.Connect(nameof(globals.GrenadeAmountChange), Callable.From(UpdateStatsText));
        }
        else
        {
            globals.HealthChange += UpdateStatsText;
            globals.LaserAmountChange += UpdateStatsText;
            globals.GrenadeAmountChange += UpdateStatsText;
        }

        laserLabel = GetNode<Label>("LaserCounter/VBoxContainer/Label");
        grenadeLabel = GetNode<Label>("GrenadeCounter/VBoxContainer/Label");
        laserIcon = GetNode<TextureRect>("LaserCounter/VBoxContainer/TextureRect");
        grenadeIcon = GetNode<TextureRect>("GrenadeCounter/VBoxContainer/TextureRect");
        healthBar = GetNode<TextureProgressBar>("MarginContainer/TextureProgressBar");

        UpdateStatsText();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void UpdateColor(int amount, TextureRect icon, Label label)
    {
        if (amount <= 0)
        {
            label.Modulate = red;
            icon.Modulate = red;
        }
        else
        {
            label.Modulate = green;
            icon.Modulate = green;
        }
    }

    public void UpdateStatsText()
    {
        laserLabel.Text = globals.LaserAmount.ToString();
        UpdateColor(globals.LaserAmount, laserIcon, laserLabel);

        grenadeLabel.Text = globals.GrenadeAmount.ToString();
        UpdateColor(globals.GrenadeAmount, grenadeIcon, grenadeLabel);

        healthBar.Value = globals.Health;
    }
}

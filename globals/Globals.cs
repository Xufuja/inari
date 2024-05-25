using Godot;
using System;

public partial class Globals : Node
{
	private int _laserAmount = 20;
    private int _grenadeAmount = 5;
    private int _health = 60;

    public int LaserAmount { get => _laserAmount; set => _laserAmount = value; }
    public int GrenadeAmount { get => _grenadeAmount; set => _grenadeAmount = value; }
    public int Health { get => _health; 
        set 
        { 
            _health = value;
            EmitSignal(SignalName.HealthChange);
        } }

    [Signal]
    public delegate void HealthChangeEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

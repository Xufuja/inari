using Godot;
using System;

public partial class Globals : Node
{
    public static bool CLASSIC_SIGNALS = true;

    private int _laserAmount = 20;
    private int _grenadeAmount = 5;
    private int _health = 60;
    private Vector2 _playerPosition;

    public int LaserAmount
    {
        get => _laserAmount;
        set
        {
            _laserAmount = value;
            EmitSignal(SignalName.LaserAmountChange);
        }
    }
    public int GrenadeAmount
    {
        get => _grenadeAmount;
        set
        {
            _grenadeAmount = value;
            EmitSignal(SignalName.GrenadeAmountChange);
        }
    }
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            EmitSignal(SignalName.HealthChange);
        }
    }

    public Vector2 PlayerPosition { get => _playerPosition; set => _playerPosition = value; }

    [Signal]
    public delegate void HealthChangeEventHandler();

    [Signal]
    public delegate void LaserAmountChangeEventHandler();

    [Signal]
    public delegate void GrenadeAmountChangeEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
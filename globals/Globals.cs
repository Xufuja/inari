using Godot;
using System;

public partial class Globals : Node
{
    public static bool CLASSIC_SIGNALS = true;

    private int _laserAmount = 20;
    private int _grenadeAmount = 5;
    private int _health = 60;
    private Vector2 _playerPosition;
    private bool playerVulnerable = true;

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
            if (value > _health)
            {
                _health = Math.Min(value, 100);
            }
            else if (playerVulnerable)
            {
                _health = value;
                playerVulnerable = false;
                PlayerInvulnerableTimer();
            }

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

    public async void PlayerInvulnerableTimer()
    {
        await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
        playerVulnerable = true;
    }
}

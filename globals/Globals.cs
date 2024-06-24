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
    private AudioStreamPlayer2D playerHitSound;

    public int LaserAmount
    {
        get => _laserAmount;
        set
        {
            _laserAmount = value;
            EmitSignal(SignalName.StatChange);
        }
    }
    public int GrenadeAmount
    {
        get => _grenadeAmount;
        set
        {
            _grenadeAmount = value;
            EmitSignal(SignalName.StatChange);
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
                playerHitSound.Play();
            }

            EmitSignal(SignalName.StatChange);
        }
    }

    public Vector2 PlayerPosition { get => _playerPosition; set => _playerPosition = value; }

    [Signal]
    public delegate void StatChangeEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerHitSound = new AudioStreamPlayer2D();
        playerHitSound.Stream = (AudioStream)GD.Load("res://audio/solid_impact.ogg");
        AddChild(playerHitSound);
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

    public object GetProperty(object target, String property)
    {
        return target.GetType().GetProperty(property).GetValue(target, null);
    }
}

using Godot;
using System;

public partial class Level : Node2D
{
	private string[] testArray = { "Test", "Hello", "Stuff" };

    public string[] TestArray { get => testArray; set => testArray = value; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
      
    }

    public void OnGatePlayerEntered(PhysicsBody2D body)
    {
        Console.WriteLine("Player Entered Gate!");
        Console.WriteLine(body);
    }

    public void OnGatePlayerExited(PhysicsBody2D body)
    {
        Console.WriteLine("Player Exited Gate!");
    }

    public void OnLaserFired()
    {
        Console.WriteLine("Laser Fired!");
    }

    public void OnGrenadeFired()
    {
        Console.WriteLine("Grenade Fired!");
    }
}

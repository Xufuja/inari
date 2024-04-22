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

    public void OnArea2DBodyEntered(PhysicsBody2D _body)
    {
        Console.WriteLine("Entered!");
    }

    public void OnArea2DBodyExited(PhysicsBody2D _body)
    {
        Console.WriteLine("Exited!");
    }

}

using Godot;
using System;
using System.Reflection;

public partial class Grenade : RigidBody2D
{
    public const int speed = 750;
    private bool explosionActive = false;
    private int explosionRadius = 400;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (explosionActive)
        {
            Godot.Collections.Array<Node> targets = (GetTree().GetNodesInGroup("Container") + GetTree().GetNodesInGroup("Entity"));

            foreach (Node target in targets)
            {
                bool inRange = ((Vector2)GetNode<Globals>("/root/Globals").GetProperty(target, "GlobalPosition")).DistanceTo(GlobalPosition) < explosionRadius;

                if (target.HasMethod("Hit") && inRange)
                {
                    Type type = target.GetType();

                    MethodInfo methodInfo = type.GetMethod("Hit");

                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(target, null);
                    }
                }
            }
        }
    }

    public void Explode()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Explosion");
        explosionActive = true;
    }

   

}

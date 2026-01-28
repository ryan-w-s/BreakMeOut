using Godot;
using System;

public partial class Paddle : CharacterBody2D
{
    [Export] public float Speed = 400.0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Vector2.Zero;

        if (Input.IsActionPressed("ui_left"))
        {
            velocity.X -= 1;
        }
        if (Input.IsActionPressed("ui_right"))
        {
            velocity.X += 1;
        }

        Velocity = velocity * Speed;
        MoveAndSlide();
        
        // Clamp to screen is handled by MoveAndSlide against walls, 
        // but can also force clamp position if needed.
        // For now relying on Walls.
    }
}

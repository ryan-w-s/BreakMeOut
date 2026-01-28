using Godot;
using System;

public partial class Ball : CharacterBody2D
{
    [Export] public float Speed = 500.0f;
    
    // Initial direction - will be randomized in game logic
    public Vector2 Direction = new Vector2(0.5f, -1.0f).Normalized();

    public override void _PhysicsProcess(double delta)
    {
        Velocity = Direction * Speed;
        KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

        if (collision != null)
        {
            // Bounce
            Direction = Direction.Bounce(collision.GetNormal());
            
            // Check what we hit
            if (collision.GetCollider() is Brick brick)
            {
                brick.Hit();
            }
            else if (collision.GetCollider() is Paddle paddle)
            {
                // Optional: Adjust angle based on paddle hit position
            }
        }
    }
}

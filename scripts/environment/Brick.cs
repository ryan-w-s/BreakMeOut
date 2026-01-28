using Godot;
using System;

public partial class Brick : StaticBody2D
{
    [Signal] public delegate void BrickDestroyedEventHandler(Vector2 position);

    [Export] public PackedScene BallScene;

    private int Health = 1;

    public void Hit()
    {
        Health--;
        if (Health <= 0)
        {
            EmitSignal(SignalName.BrickDestroyed, GlobalPosition);
            
            // Spawn new ball
            if (BallScene != null)
            {
                var ball = BallScene.Instantiate<Node2D>();
                ball.GlobalPosition = GlobalPosition;
                // Add to main scene root or a container if possible
                GetTree().CurrentScene.CallDeferred("add_child", ball);
            }
            
            GetNode<GameManager>("/root/GameManager").AddScore(100);
            QueueFree();
        }
    }
}

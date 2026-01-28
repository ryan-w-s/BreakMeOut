using Godot;
using System;

public partial class DeathZone : Area2D
{
    // Need reference to GameManager - usually via Autoload
    private GameManager _gameManager;

    public override void _Ready()
    {
        _gameManager = GetNode<GameManager>("/root/GameManager");
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Ball)
        {
            var balls = GetTree().GetNodesInGroup("Balls");
            if (balls.Count <= 1)
            {
                _gameManager?.LoseLife();
            }
            body.QueueFree(); // Destroy the ball
        }
    }
}

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
            body.QueueFree(); // Destroy the ball
            _gameManager?.LoseLife();
        }
    }
}

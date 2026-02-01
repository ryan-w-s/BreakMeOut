using Godot;
using System;

public partial class Main : Node
{
    [Export] public PackedScene BallScene;

    public override void _Ready()
    {
        var gm = GetNode<GameManager>("/root/GameManager");
        if (gm != null)
        {
            gm.LivesChanged += OnLivesChanged;
            gm.GameOver += OnGameOver;
        }
    }

    public override void _ExitTree()
    {
        var gm = GetNode<GameManager>("/root/GameManager");
        if (gm != null)
        {
            gm.LivesChanged -= OnLivesChanged;
            gm.GameOver -= OnGameOver;
        }
    }

    private void OnLivesChanged(int lives)
    {
        if (lives > 0)
        {
            CallDeferred("RespawnBall");
        }
    }

    private void RespawnBall()
    {
        // Check if any valid balls exist (ignoring those about to be deleted)
        var balls = GetTree().GetNodesInGroup("Balls");
        foreach (Node ball in balls)
        {
            if (IsInstanceValid(ball) && !ball.IsQueuedForDeletion())
            {
                return;
            }
        }

        if (BallScene != null)
        {
            var ball = BallScene.Instantiate<Node2D>();
            ball.GlobalPosition = new Vector2(640, 600);
            AddChild(ball);
        }
    }

    private void OnGameOver(bool victory)
    {
        GetTree().ChangeSceneToFile("res://scenes/ui/GameOver.tscn");
    }
}

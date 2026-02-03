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
            gm.ResetGame();
            gm.LivesChanged += OnLivesChanged;
            gm.GameOver += OnGameOver;
        }

        // Attach initial ball if present
        CallDeferred("AttachInitialBall");
    }

    private void AttachInitialBall()
    {
        var paddle = GetNodeOrNull<Paddle>("Paddle");
        // If not direct child, try finding by type/group
        if (paddle == null)
        {
             paddle = GetNodeOrNull<Paddle>("../Paddle") ?? (Paddle)GetTree().GetFirstNodeInGroup("Paddle");
        }

        var ball = GetNodeOrNull<Ball>("Ball"); // Assuming named "Ball" in scene
        if (ball == null)
        {
             ball = (Ball)GetTree().GetFirstNodeInGroup("Balls");
        }

        if (paddle != null && ball != null)
        {
            ball.AttachToPaddle(paddle);
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
        foreach (Node ballNode in balls)
        {
            if (IsInstanceValid(ballNode) && !ballNode.IsQueuedForDeletion())
            {
                return;
            }
        }

        if (BallScene != null)
        {
            // Find Paddle
            var paddle = GetNodeOrNull<Paddle>("Paddle");
            // If not direct child, try finding by type/group
            if (paddle == null)
            {
                // Assuming Paddle is a unique node in the scene for now
                paddle = GetNodeOrNull<Paddle>("../Paddle") ?? (Paddle)GetTree().GetFirstNodeInGroup("Paddle");
            }

            if (paddle != null && IsInstanceValid(paddle))
            {
                var ballInstance = BallScene.Instantiate<Ball>();
                ballInstance.AddToGroup("Balls"); // Ensure it's in the group
                AddChild(ballInstance);
                ballInstance.AttachToPaddle(paddle);
            }
            else
            {
                GD.PrintErr("Main: Could not find Paddle to attach new Ball to!");
            }
        }
    }

    private void OnGameOver(bool victory)
    {
        GetTree().ChangeSceneToFile("res://scenes/ui/GameOver.tscn");
    }
}

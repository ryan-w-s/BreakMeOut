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
            UpdateBackground(gm.Progression.CurrentLevelPath);
            gm.ResetGame();
            gm.LivesChanged += OnLivesChanged;
            gm.GameOver += OnGameOver;
        }

        // Attach initial ball if present
        CallDeferred("AttachInitialBall");
    }

    private void UpdateBackground(string levelPath)
    {
        // Default to city theme
        string texturePath = "res://assets/art/backgrounds/level_bg_city.jpg";
        Color modulateColor = Colors.White;

        // Try to parse level number
        // path format: res://levels/level{N}.json
        if (!string.IsNullOrEmpty(levelPath))
        {
            try
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(levelPath); // level1
                string numberPart = filename.Replace("level", "");
                if (int.TryParse(numberPart, out int levelNum))
                {
                    // Map levels 1-9 to themes
                    // 1, 4, 7 -> City (Theme 1)
                    // 2, 5, 8 -> Matrix (Theme 2)
                    // 3, 6, 9 -> Space (Theme 3)
                    
                    int themeIndex = (levelNum - 1) % 3; // 0, 1, 2
                    
                    switch (themeIndex)
                    {
                        case 0: // City
                            texturePath = "res://assets/art/backgrounds/level_bg_city.jpg";
                            // Variations
                            if (levelNum > 3) modulateColor = new Color(0.8f, 0.8f, 1.0f); // Slight blue tint
                            if (levelNum > 6) modulateColor = new Color(1.0f, 0.8f, 1.0f); // Slight pink tint
                            break;
                        case 1: // Matrix
                            texturePath = "res://assets/art/backgrounds/level_bg_matrix.jpg";
                            if (levelNum > 3) modulateColor = new Color(1.0f, 0.5f, 0.5f); // Red Matrix
                            if (levelNum > 6) modulateColor = new Color(0.5f, 0.5f, 1.0f); // Blue Matrix
                            break;
                        case 2: // Space
                            texturePath = "res://assets/art/backgrounds/level_bg_space.jpg";
                            if (levelNum > 3) modulateColor = new Color(0.8f, 0.5f, 1.0f); // Purple Space
                            if (levelNum > 6) modulateColor = new Color(1.0f, 0.9f, 0.5f); // Gold Space
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                GD.PrintErr($"Error parsing level number for background: {e.Message}");
            }
        }

        var bgRect = GetNodeOrNull<TextureRect>("Background");
        if (bgRect != null)
        {
            var texture = GD.Load<Texture2D>(texturePath);
            if (texture != null)
            {
                bgRect.Texture = texture;
                bgRect.Modulate = modulateColor;
            }
            else
            {
                GD.PrintErr($"BACKGROUND ERROR: Failed to load texture at {texturePath}");
            }
        }
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

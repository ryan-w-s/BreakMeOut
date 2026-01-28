using Godot;
using System;

public partial class HUD : CanvasLayer
{
    // These will be linked in editor or found by name
    private Label _scoreLabel;
    private Label _livesLabel;

    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>("ScoreLabel");
        _livesLabel = GetNode<Label>("LivesLabel");
        
        var gm = GetNode<GameManager>("/root/GameManager");
        if (gm != null)
        {
            gm.ScoreChanged += UpdateScore;
            gm.LivesChanged += UpdateLives;
            // Initialize
            UpdateScore(gm.Score);
            UpdateLives(gm.Lives);
        }
    }

    public void UpdateScore(int score)
    {
        if (_scoreLabel != null)
            _scoreLabel.Text = $"Score: {score}";
    }

    public void UpdateLives(int lives)
    {
        if (_livesLabel != null)
            _livesLabel.Text = $"Lives: {lives}";
    }
}

using Godot;
using System;

public partial class GameManager : Node
{
    [Signal] public delegate void ScoreChangedEventHandler(int newScore);
    [Signal] public delegate void LivesChangedEventHandler(int newLives);
    [Signal] public delegate void LevelCompletedEventHandler();
    [Signal] public delegate void GameOverEventHandler(bool win);

    public int Score { get; private set; } = 0;
    public int Lives { get; private set; } = 3;
    public int BricksRemaining { get; private set; } = 0;
    public bool IsVictory { get; private set; } = false;

    public override void _Ready()
    {
        GD.Print("GameManager Initialized");
    }

    public void RegisterBrick()
    {
        BricksRemaining++;
    }

    public void BrickDestroyed()
    {
        BricksRemaining--;
        if (BricksRemaining <= 0)
        {
            IsVictory = true;
            EmitSignal(SignalName.GameOver, true);
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;
        EmitSignal(SignalName.ScoreChanged, Score);
    }

    public void LoseLife()
    {
        Lives--;
        EmitSignal(SignalName.LivesChanged, Lives);
        if (Lives <= 0)
        {
            IsVictory = false;
            EmitSignal(SignalName.GameOver, false);
        }
    }

    public void ResetGame()
    {
        Score = 0;
        Lives = 3;
        BricksRemaining = 0;
        IsVictory = false;
        EmitSignal(SignalName.ScoreChanged, Score);
        EmitSignal(SignalName.LivesChanged, Lives);
    }
}

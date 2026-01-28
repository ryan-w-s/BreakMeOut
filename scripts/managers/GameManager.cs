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

    public override void _Ready()
    {
        GD.Print("GameManager Initialized");
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
            EmitSignal(SignalName.GameOver, false);
        }
    }

    public void ResetGame()
    {
        Score = 0;
        Lives = 3;
        EmitSignal(SignalName.ScoreChanged, Score);
        EmitSignal(SignalName.LivesChanged, Lives);
    }
}

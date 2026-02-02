using Godot;
using System;
using System.Collections.Generic;
using BreakMeOut.Scripts.Models;
using BreakMeOut.Scripts.Utils;

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

	public ProgressionState Progression { get; private set; } = new ProgressionState();

	public override void _Ready()
	{
		GD.Print("GameManager Initialized");
        Progression = ProgressionService.LoadProgress();
	}

	public void UnlockLevel(string path)
	{
		Progression.UnlockLevel(path);
        ProgressionService.SaveProgress(Progression);
	}

	public void RegisterBrick()
	{
		BricksRemaining++;
	}

	public void BrickDestroyed()
	{
		BricksRemaining--;
		// Verify against actual brick count in scene to prevent race conditions
		var actualBrickCount = GetTree().GetNodesInGroup("bricks").Count;
		if (actualBrickCount <= 0)
		{
			IsVictory = true;
			UnlockNextLevel();
			EmitSignal(SignalName.GameOver, true);
		}
	}

	public void UnlockNextLevel()
	{
		string current = Progression.CurrentLevelPath;
		var levelFiles = ProgressionService.GetLevelFiles();

		int currentIndex = levelFiles.IndexOf(current);
		if (currentIndex != -1 && currentIndex < levelFiles.Count - 1)
		{
			string nextLevel = levelFiles[currentIndex + 1];
			UnlockLevel(nextLevel);
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
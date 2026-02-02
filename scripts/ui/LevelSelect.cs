using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class LevelSelect : Control
{
	private GridContainer _grid;

	public override void _Ready()
	{
		_grid = GetNode<GridContainer>("VBoxContainer/GridContainer");
		var backBtn = GetNode<Button>("VBoxContainer/BackButton");
		
		if (backBtn != null)
		{
			backBtn.Pressed += OnBackPressed;
		}

		PopulateLevels();
	}

	private void PopulateLevels()
	{
		if (_grid == null) return;

		// Clear existing buttons
		foreach (Node child in _grid.GetChildren())
		{
			child.QueueFree();
		}

		var levelFiles = BreakMeOut.Scripts.Utils.ProgressionService.GetLevelFiles();
		var gm = GetNode<GameManager>("/root/GameManager");

		foreach (string path in levelFiles)
		{
			Button btn = new Button();
			btn.Text = Path.GetFileNameWithoutExtension(path).Replace("level", "Level ");
			btn.CustomMinimumSize = new Vector2(100, 100);
			
			bool isUnlocked = gm.Progression.UnlockedLevels.Contains(path);
			btn.Disabled = !isUnlocked;
			
			if (isUnlocked)
			{
				btn.Pressed += () => OnLevelSelected(path);
			}
			
			_grid.AddChild(btn);
		}
	}

	private void OnLevelSelected(string path)
	{
		var gm = GetNode<GameManager>("/root/GameManager");
		gm.Progression.CurrentLevelPath = path;
		GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
	}

	public void OnBackPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/ui/MainMenu.tscn");
	}
}

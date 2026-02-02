using Godot;
using System;
using System.Collections.Generic;
using BreakMeOut.Scripts.Utils;

public partial class GameOver : Control
{
    public override void _Ready()
    {
        var gm = GetNode<GameManager>("/root/GameManager");
        var label = GetNode<Label>("VBoxContainer/Label");
        var nextBtn = GetNode<Button>("VBoxContainer/NextLevelButton");
        var selectBtn = GetNode<Button>("VBoxContainer/LevelSelectButton");
        
        if (gm.IsVictory)
        {
            label.Text = "YOU WIN!";
            label.Modulate = new Color(0, 1, 0); // Green
            
            var levelFiles = ProgressionService.GetLevelFiles();
            int currentIndex = levelFiles.IndexOf(gm.Progression.CurrentLevelPath);
            if (currentIndex != -1 && currentIndex < levelFiles.Count - 1)
            {
                nextBtn.Visible = true;
                nextBtn.Pressed += OnNextLevelPressed;
            }
        }
        else
        {
            label.Text = "GAME OVER";
            label.Modulate = new Color(1, 0, 0); // Red
        }

        GetNode<Button>("VBoxContainer/RestartButton").Pressed += OnRestartPressed;
        GetNode<Button>("VBoxContainer/MenuButton").Pressed += OnMenuPressed;
        selectBtn.Pressed += OnLevelSelectPressed;
    }

    private void OnNextLevelPressed()
    {
        var gm = GetNode<GameManager>("/root/GameManager");
        var levelFiles = ProgressionService.GetLevelFiles();
        int currentIndex = levelFiles.IndexOf(gm.Progression.CurrentLevelPath);
        if (currentIndex != -1 && currentIndex < levelFiles.Count - 1)
        {
            gm.Progression.CurrentLevelPath = levelFiles[currentIndex + 1];
            gm.ResetGame();
            GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
        }
    }

    private void OnRestartPressed()
    {
        GetNode<GameManager>("/root/GameManager").ResetGame();
        GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
    }

    private void OnLevelSelectPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/ui/LevelSelect.tscn");
    }

    private void OnMenuPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/ui/MainMenu.tscn");
    }
}
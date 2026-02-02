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
        PopulateLevels();
    }

    private void PopulateLevels()
    {
        // Clear existing buttons
        foreach (Node child in _grid.GetChildren())
        {
            child.QueueFree();
        }

        string levelsDir = "res://levels/";
        using var dir = DirAccess.Open(levelsDir);
        if (dir != null)
        {
            dir.ListDirBegin();
            string fileName = dir.GetNext();
            
            List<string> levelFiles = new List<string>();
            while (fileName != "")
            {
                if (!dir.CurrentIsDir() && fileName.EndsWith(".json"))
                {
                    levelFiles.Add(levelsDir + fileName);
                }
                fileName = dir.GetNext();
            }
            levelFiles.Sort();

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
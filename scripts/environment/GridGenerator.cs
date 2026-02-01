using Godot;
using System;
using BreakMeOut.Scripts.Utils;
using BreakMeOut.Scripts.Models;

public partial class GridGenerator : Node2D
{
    [Export] public PackedScene BrickScene;
    [Export] public int Rows = 10;
    [Export] public string LevelPath = "res://levels/level1.json";
    
    private const float BaseBrickSize = 60f;
    
    public override void _Ready()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (BrickScene == null)
        {
            GD.PrintErr("GridGenerator: BrickScene is not assigned!");
            return;
        }

        LevelData level = LoadLevel();
        if (level == null)
        {
            GD.PrintErr("GridGenerator: Failed to load level data.");
            return;
        }

        int screenWidth = 1280;
        int spacing = 5;

        var layout = BrickLayoutCalculator.CalculateLayout(screenWidth, spacing);
        
        GD.Print($"Generating Grid: Cols={level.Columns}, Rows={level.Rows}, Width={layout.BrickWidth}, MarginX={layout.MarginX}");

        int startY = 50; // Top margin

        float scale = layout.BrickWidth / BaseBrickSize;
        Vector2 scaleVec = new Vector2(scale, scale);
        float rowStep = layout.BrickWidth + spacing;
        float halfSize = layout.BrickWidth / 2f;

        for (int r = 0; r < level.Rows; r++)
        {
            if (r >= level.BrickGrid.Length) break;

            for (int c = 0; c < level.Columns; c++)
            {
                if (c >= level.BrickGrid[r].Length) break;

                int health = level.BrickGrid[r][c];
                if (health <= 0) continue;

                var brickNode = BrickScene.Instantiate<Node2D>();
                AddChild(brickNode);

                float x = layout.MarginX + c * (layout.BrickWidth + spacing);
                float y = startY + r * rowStep;

                brickNode.Position = new Vector2(x + halfSize, y + halfSize);
                brickNode.Scale = scaleVec;

                if (brickNode is Brick brick)
                {
                    brick.SetHealth(health);
                }
            }
        }
    }

    private LevelData LoadLevel()
    {
        try
        {
            if (!FileAccess.FileExists(LevelPath))
            {
                GD.PrintErr($"Level file not found: {LevelPath}");
                return null;
            }

            using var file = FileAccess.Open(LevelPath, FileAccess.ModeFlags.Read);
            string json = file.GetAsText();
            return LevelSerializer.Deserialize(json);
        }
        catch (Exception e)
        {
            GD.PrintErr($"Error loading level: {e.Message}");
            return null;
        }
    }
}
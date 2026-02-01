using Godot;
using System;
using BreakMeOut.Scripts.Utils;

public partial class GridGenerator : Node2D
{
    [Export] public PackedScene BrickScene;
    [Export] public int Rows = 10;
    
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

        // Get viewport size
        int screenWidth = 1280; // Hardcoded for now as per spec/calculator logic
        int spacing = 5;

        var layout = BrickLayoutCalculator.CalculateLayout(screenWidth, spacing);
        
        GD.Print($"Generating Grid: Cols={layout.Columns}, Width={layout.BrickWidth}, MarginX={layout.MarginX}");

        int startY = 100; // Top margin

        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < layout.Columns; c++)
            {
                var brick = BrickScene.Instantiate<Node2D>();
                AddChild(brick);

                float x = layout.MarginX + c * (layout.BrickWidth + spacing);
                float y = startY + r * (25 + spacing); // 25 is hardcoded height for now

                brick.Position = new Vector2(x, y);
                
                // Scale the brick visually if the sprite isn't already 75x25
                // Assuming the Brick scene has a Sprite2D/ColorRect that needs scaling?
                // Or we just rely on the Brick's default size.
                // For this prototype, we'll assume the Brick scene needs to be adjusted or we scale it.
                // Let's print the position for verification.
            }
        }
    }
}

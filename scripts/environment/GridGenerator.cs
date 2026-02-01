using Godot;
using System;
using BreakMeOut.Scripts.Utils;

public partial class GridGenerator : Node2D
{
    [Export] public PackedScene BrickScene;
    [Export] public int Rows = 10;
    
    // Base size of the Brick scene content (ColorRect/CollisionShape)
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

        int screenWidth = 1280;
        int spacing = 5;

        var layout = BrickLayoutCalculator.CalculateLayout(screenWidth, spacing);
        
        GD.Print($"Generating Grid: Cols={layout.Columns}, Width={layout.BrickWidth}, MarginX={layout.MarginX}");

        int startY = 100; // Top margin

        // Calculate scale factor
        // Target is layout.BrickWidth (e.g., 40)
        // Base is 60.
        float scale = layout.BrickWidth / BaseBrickSize;
        Vector2 scaleVec = new Vector2(scale, scale);
        
        // Effective height including spacing
        // Visual height = layout.BrickWidth (since 40x40)
        float rowStep = layout.BrickWidth + spacing;

        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < layout.Columns; c++)
            {
                var brick = BrickScene.Instantiate<Node2D>();
                AddChild(brick);

                float x = layout.MarginX + c * (layout.BrickWidth + spacing);
                float y = startY + r * rowStep;

                // Adjust position to be centered? 
                // In Brick.tscn, ColorRect is offset -30, -30. So (0,0) is center.
                // Our grid logic usually assumes top-left or center?
                // If we position at (x,y), and (0,0) is center, the brick will be centered at (x,y).
                // But our calculation `x` is the *left edge* of the column if we assume `c * width`.
                // If `c=0`, `x = Margin`. If we place center there, the left half overlaps the margin.
                // We should shift by `half width`.
                
                float halfSize = layout.BrickWidth / 2f;
                brick.Position = new Vector2(x + halfSize, y + halfSize);
                brick.Scale = scaleVec;
            }
        }
    }
}
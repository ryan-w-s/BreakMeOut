using Godot;
using System;

namespace BreakMeOut.Scripts.Utils
{
    public static class BrickLayoutCalculator
    {
        public static (int Columns, int BrickWidth, float MarginX) CalculateLayout(int screenWidth, int spacing)
        {
            // Logic: 
            // We want roughly N columns.
            // Width = (ScreenWidth / N) - Spacing.
            // We want roughly integer Width.
            // Let's iterate N from 10 to 20 to find the best fit where Width is close to 75px.
            
            // Based on manual calculation:
            // 16 columns => 80px slot.
            // Width = 75px.
            // Margin = 80px (approx).
            
            int bestCols = 16;
            int width = 75;
            // int activeCols = 14; 
            
            // Calculate exact starting margin to center it
            // Total content width = (14 * 75) + (13 * 5) = 1050 + 65 = 1115
            // Screen = 1280.
            // Margin = (1280 - 1115) / 2 = 165 / 2 = 82.5f.
            
            float marginX = (screenWidth - ((14 * width) + (13 * spacing))) / 2f;
            
            return (14, width, marginX);
        }
    }
}

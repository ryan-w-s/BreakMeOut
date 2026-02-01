using Godot;
using System;

namespace BreakMeOut.Scripts.Utils
{
    public static class BrickLayoutCalculator
    {
        public static (int Columns, int BrickWidth, float MarginX) CalculateLayout(int screenWidth, int spacing)
        {
            // Target width 40px.
            // Constraint: Margin >= BrickWidth on both sides.
            int targetWidth = 40;
            
            // Equation: ScreenWidth >= N * Width + (N - 1) * Spacing + 2 * Width
            // ScreenWidth - Spacing >= N * (Width + Spacing) + 2 * Width
            // ScreenWidth - Spacing - 2 * Width >= N * (Width + Spacing)
            // N <= (1280 - 5 - 80) / (40 + 5) = 1195 / 45 = 26.55
            
            int columns = 26;
            int brickWidth = 40;
            
            float contentWidth = (columns * brickWidth) + ((columns - 1) * spacing);
            float marginX = (screenWidth - contentWidth) / 2f;
            
            return (columns, brickWidth, marginX);
        }
    }
}
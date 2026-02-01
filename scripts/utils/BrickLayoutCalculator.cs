using Godot;
using System;

namespace BreakMeOut.Scripts.Utils
{
    public static class BrickLayoutCalculator
    {
        public static (int Columns, int BrickWidth, float MarginX) CalculateLayout(int screenWidth, int spacing)
        {
            // Target width 40px.
            int brickWidth = 40;
            
            float contentWidth = (columns * brickWidth) + ((columns - 1) * spacing);
            float marginX = (screenWidth - contentWidth) / 2f;
            
            return (columns, brickWidth, marginX);
        }
    }
}
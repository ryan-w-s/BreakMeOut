using System;

namespace BreakMeOut.Scripts.Models
{
    public class LevelData
    {
        public string Name { get; set; } = "Untitled Level";
        public int Rows { get; set; }
        public int Columns { get; set; }
        
        // Jagged array for JSON serialization compatibility
        // Format: [RowIndex][ColumnIndex] -> BrickHealth (0 for empty)
        public int[][] BrickGrid { get; set; }
    }
}

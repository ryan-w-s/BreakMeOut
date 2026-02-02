using System.Collections.Generic;

namespace BreakMeOut.Scripts.Models
{
	public class ProgressionState
	{
		public string CurrentLevelPath { get; set; } = "res://levels/level1.json";
		public List<string> UnlockedLevels { get; set; } = new List<string> { "res://levels/level1.json" };

		public void UnlockLevel(string path)
		{
			if (!UnlockedLevels.Contains(path))
			{
				UnlockedLevels.Add(path);
			}
		}
	}
}

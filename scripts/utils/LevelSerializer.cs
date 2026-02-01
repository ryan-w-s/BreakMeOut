using BreakMeOut.Scripts.Models;
using System.Text.Json;

namespace BreakMeOut.Scripts.Utils
{
    public static class LevelSerializer
    {
        public static string Serialize(LevelData data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(data, options);
        }

        public static LevelData Deserialize(string json)
        {
            return JsonSerializer.Deserialize<LevelData>(json);
        }
    }
}
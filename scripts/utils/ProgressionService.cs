using BreakMeOut.Scripts.Models;
using Godot;
using System;
using System.Text.Json;

namespace BreakMeOut.Scripts.Utils
{
    public static class ProgressionService
    {
        private const string SAVE_PATH = "user://savegame.json";

        public static string Serialize(ProgressionState state)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(state, options);
        }

        public static ProgressionState Deserialize(string json)
        {
            try 
            {
                return JsonSerializer.Deserialize<ProgressionState>(json);
            }
            catch
            {
                return null;
            }
        }

        public static void SaveProgress(ProgressionState state)
        {
            try
            {
                string json = Serialize(state);
                using var file = FileAccess.Open(SAVE_PATH, FileAccess.ModeFlags.Write);
                if (file == null)
                {
                    GD.PrintErr($"Failed to open save file for writing: {FileAccess.GetOpenError()}");
                    return;
                }
                file.StoreString(json);
            }
            catch (Exception e)
            {
                GD.PrintErr($"Error saving progress: {e.Message}");
            }
        }

        public static ProgressionState LoadProgress()
        {
            if (!FileAccess.FileExists(SAVE_PATH))
            {
                return new ProgressionState(); // Default state
            }

            try
            {
                using var file = FileAccess.Open(SAVE_PATH, FileAccess.ModeFlags.Read);
                if (file == null)
                {
                    GD.PrintErr($"Failed to open save file for reading: {FileAccess.GetOpenError()}");
                    return new ProgressionState();
                }
                string json = file.GetAsText();
                var state = Deserialize(json);
                return state ?? new ProgressionState();
            }
            catch (Exception e)
            {
                GD.PrintErr($"Error loading progress: {e.Message}");
                return new ProgressionState();
            }
        }
    }
}

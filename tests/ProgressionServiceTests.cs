using NUnit.Framework;
using BreakMeOut.Scripts.Models;
using BreakMeOut.Scripts.Utils;
using System.Collections.Generic;

namespace BreakMeOut.Tests
{
    [TestFixture]
    public class ProgressionServiceTests
    {
        [Test]
        public void Serialize_ShouldReturnJsonString()
        {
            var state = new ProgressionState();
            state.UnlockLevel("res://levels/level2.json");
            
            string json = ProgressionService.Serialize(state);
            
            Assert.That(json, Does.Contain("res://levels/level2.json"));
            Assert.That(json, Does.Contain("UnlockedLevels"));
        }

        [Test]
        public void Deserialize_ShouldReturnStateObject()
        {
            string json = "{ \"UnlockedLevels\": [ \"res://levels/level1.json\", \"res://levels/level3.json\" ], \"CurrentLevelPath\": \"res://levels/level3.json\" }";
            
            var state = ProgressionService.Deserialize(json);
            
            Assert.That(state, Is.Not.Null);
            Assert.That(state.UnlockedLevels, Contains.Item("res://levels/level3.json"));
            Assert.That(state.CurrentLevelPath, Is.EqualTo("res://levels/level3.json"));
        }

        [Test]
        public void Deserialize_InvalidJson_ShouldReturnDefaultState() // Or null? Let's say null for now
        {
            string json = "invalid json";
            var state = ProgressionService.Deserialize(json);
            Assert.That(state, Is.Null);
        }
    }
}

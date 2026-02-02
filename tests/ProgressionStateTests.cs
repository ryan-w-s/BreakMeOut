using NUnit.Framework;
using BreakMeOut.Scripts.Models;
using System.Collections.Generic;

namespace BreakMeOut.Tests
{
    [TestFixture]
    public class ProgressionStateTests
    {
        private ProgressionState _state;

        [SetUp]
        public void Setup()
        {
            _state = new ProgressionState();
        }

        [Test]
        public void CurrentLevelPath_DefaultValue_ShouldBeLevel1()
        {
            Assert.That(_state.CurrentLevelPath, Is.EqualTo("res://levels/level1.json"));
        }

        [Test]
        public void UnlockedLevels_ShouldContainLevel1ByDefault()
        {
            Assert.That(_state.UnlockedLevels, Contains.Item("res://levels/level1.json"));
        }

        [Test]
        public void UnlockLevel_ShouldAddLevelToList()
        {
            string newLevel = "res://levels/level2.json";
            _state.UnlockLevel(newLevel);
            Assert.That(_state.UnlockedLevels, Contains.Item(newLevel));
        }

        [Test]
        public void UnlockLevel_ShouldNotAddDuplicates()
        {
            string newLevel = "res://levels/level2.json";
            _state.UnlockLevel(newLevel);
            _state.UnlockLevel(newLevel);
            
            int count = 0;
            foreach(var level in _state.UnlockedLevels)
            {
                if (level == newLevel) count++;
            }
            Assert.That(count, Is.EqualTo(1));
        }
    }
}

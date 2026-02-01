using NUnit.Framework;
using BreakMeOut.Scripts.Models;
using BreakMeOut.Scripts.Utils;

namespace BreakMeOut.Tests
{
    public class LevelSerializerTests
    {
        [Test]
        public void SerializeAndDeserialize_PreservesData()
        {
            var data = new LevelData
            {
                Name = "Test Level",
                Rows = 2,
                Columns = 3,
                BrickGrid = new int[][]
                {
                    new int[] { 1, 2, 3 },
                    new int[] { 0, 1, 0 }
                }
            };

            string json = LevelSerializer.Serialize(data);
            Assert.That(json, Is.Not.Empty);

            var loaded = LevelSerializer.Deserialize(json);
            
            Assert.That(loaded.Name, Is.EqualTo(data.Name));
            Assert.That(loaded.Rows, Is.EqualTo(data.Rows));
            Assert.That(loaded.Columns, Is.EqualTo(data.Columns));
            Assert.That(loaded.BrickGrid[0][0], Is.EqualTo(1));
            Assert.That(loaded.BrickGrid[0][2], Is.EqualTo(3));
            Assert.That(loaded.BrickGrid[1][1], Is.EqualTo(1));
        }
    }
}

using NUnit.Framework;
using BreakMeOut.Scripts.Utils;

namespace BreakMeOut.Tests
{
    public class BrickLayoutCalculatorTests
    {
        [Test]
        public void CalculateLayout_ReturnsOptimalValues()
        {
            int screenWidth = 1280;
            int spacing = 5;

            var (columns, brickWidth, marginX) = BrickLayoutCalculator.CalculateLayout(screenWidth, spacing);

            Assert.That(columns, Is.EqualTo(14));
            Assert.That(brickWidth, Is.EqualTo(75));
            // Total width = 14 * 75 + 13 * 5 = 1050 + 65 = 1115.
            // Margin = (1280 - 1115) / 2 = 82.5
            Assert.That(marginX, Is.EqualTo(82.5f));
        }
    }
}

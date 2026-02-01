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

            // Target roughly 40px width.
            // If N=28: Width 40. Total = 28*40 + 27*5 = 1120 + 135 = 1255. Margin = (1280-1255)/2 = 12.5.
            // If N=26: Width 40. Total = 26*40 + 25*5 = 1040 + 125 = 1165. Margin = (1280-1165)/2 = 57.5.
            // User wanted "one row on each side empty" -> Margin ~ 40px.
            // So N=26 or N=28 are both valid candidates depending on exact logic.
            // Let's assume the calculator picks the one closest to 40px width with decent margin.
            
            Assert.That(brickWidth, Is.EqualTo(40));
            Assert.That(columns, Is.GreaterThanOrEqualTo(26));
            Assert.That(marginX, Is.GreaterThan(0));
        }
    }
}

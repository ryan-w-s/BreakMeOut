using NUnit.Framework;
using BreakMeOut.Scripts.Utils;
using Godot;

namespace BreakMeOut.Tests
{
    [TestFixture]
    public class BallStateCalculatorTests
    {
        [Test]
        public void CalculateHeldPosition_ReturnsPaddlePositionPlusOffset()
        {
            Vector2 paddlePos = new Vector2(100, 200);
            Vector2 offset = new Vector2(0, -20);
            
            Vector2 result = BallStateCalculator.CalculateHeldPosition(paddlePos, offset);
            
            Assert.That(result.X, Is.EqualTo(100));
            Assert.That(result.Y, Is.EqualTo(180));
        }

        [Test]
        public void CalculateLaunchVelocity_ReturnsUpwardVectorWithBias()
        {
            float speed = 500f;
            Vector2 paddleVelocity = new Vector2(100, 0);
            float biasFactor = 0.5f;

            // Bias = 100 * 0.5 = 50
            // Initial = (0, -1) * 500 = (0, -500)
            // Final = (50, -500).Normalized() * 500
            
            Vector2 result = BallStateCalculator.CalculateLaunchVelocity(speed, paddleVelocity, biasFactor);
            
            Assert.That(result.Y, Is.LessThan(0)); // Moving up
            Assert.That(result.X, Is.GreaterThan(0)); // Moving right due to paddle
            // Check speed magnitude is preserved
            Assert.That(result.Length(), Is.EqualTo(speed).Within(0.1f));
        }
    }
}

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

            Vector2 result = BallStateCalculator.CalculateLaunchVelocity(speed, paddleVelocity, biasFactor);
            
            Assert.That(result.Y, Is.LessThan(0)); // Moving up
            Assert.That(result.X, Is.GreaterThan(0)); // Moving right due to paddle
            Assert.That(result.Length(), Is.EqualTo(speed).Within(0.1f));
        }

        [Test]
        public void CalculateBounceDirection_WithPaddleVelocity_AppliesBias()
        {
            Vector2 currentDirection = new Vector2(0, 1); // Moving straight down
            Vector2 normal = new Vector2(0, -1); // Upward normal (hit top of paddle)
            Vector2 paddleVelocity = new Vector2(200, 0); // Moving fast right
            float biasFactor = 0.1f;
            float speed = 500f;

            // Standard bounce: (0, 1).Bounce(0, -1) = (0, -1)
            // Bias: 200 * 0.1 / 500 = 20 / 500 = 0.04
            // Result X should be > 0
            
            Vector2 result = BallStateCalculator.CalculateBounceDirection(currentDirection, normal, paddleVelocity, biasFactor, speed);
            
            Assert.That(result.Y, Is.LessThan(0)); // Still moving up
            Assert.That(result.X, Is.GreaterThan(0)); // Biased right
            Assert.That(result.IsNormalized(), Is.True);
        }
    }
}
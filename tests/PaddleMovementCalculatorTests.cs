using NUnit.Framework;
using BreakMeOut.Scripts.Utils;
using System;

namespace BreakMeOut.Tests
{
    [TestFixture]
    public class PaddleMovementCalculatorTests
    {
        [Test]
        public void UpdatePosition_MouseMoved_FollowsMouse()
        {
            float currentX = 100f;
            float mouseX = 200f;
            float lastMouseX = 100f; // Mouse moved from 100 to 200
            bool left = false;
            bool right = false;
            float speed = 100f;
            float delta = 1f;
            float min = 0f;
            float max = 1000f;

            float newX = PaddleMovementCalculator.UpdatePosition(currentX, mouseX, lastMouseX, left, right, speed, delta, min, max);

            Assert.That(newX, Is.EqualTo(mouseX));
        }

        [Test]
        public void UpdatePosition_MouseStationary_KeysMove()
        {
            float currentX = 100f;
            float mouseX = 100f;
            float lastMouseX = 100f; // Mouse didn't move
            bool left = false;
            bool right = true; // Moving right
            float speed = 50f;
            float delta = 1f;
            float min = 0f;
            float max = 1000f;

            // Expected: 100 + 50 * 1 = 150
            float newX = PaddleMovementCalculator.UpdatePosition(currentX, mouseX, lastMouseX, left, right, speed, delta, min, max);

            Assert.That(newX, Is.EqualTo(150f));
        }

        [Test]
        public void UpdatePosition_ClampsToMin()
        {
            float currentX = 10f;
            float mouseX = -50f;
            float lastMouseX = 10f; 
            bool left = false;
            bool right = false;
            float speed = 50f;
            float delta = 1f;
            float min = 0f;
            float max = 1000f;

            // Mouse moved to -50, should clamp to 0
            float newX = PaddleMovementCalculator.UpdatePosition(currentX, mouseX, lastMouseX, left, right, speed, delta, min, max);

            Assert.That(newX, Is.EqualTo(min));
        }

        [Test]
        public void CalculateVelocity_ReturnsCorrectSpeed()
        {
            float currentX = 100f;
            float targetX = 150f;
            float delta = 0.5f;

            // Velocity = (150 - 100) / 0.5 = 50 / 0.5 = 100
            float vel = PaddleMovementCalculator.CalculateVelocity(currentX, targetX, delta);

            Assert.That(vel, Is.EqualTo(100f));
        }
    }
}
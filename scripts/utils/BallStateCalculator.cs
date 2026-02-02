using Godot;

namespace BreakMeOut.Scripts.Utils
{
    public static class BallStateCalculator
    {
        public static Vector2 CalculateHeldPosition(Vector2 paddlePosition, Vector2 offset)
        {
            return paddlePosition + offset;
        }

        public static Vector2 CalculateLaunchVelocity(float speed, Vector2 paddleVelocity, float biasFactor)
        {
            Vector2 baseDir = Vector2.Up;
            // Add bias based on paddle velocity X
            float biasX = paddleVelocity.X * biasFactor;
            
            // We want to add this bias to the direction, then normalize
            // To keep it consistent, let's treat bias as a vector component addition to the Up vector, then normalize.
            // Note: Paddle velocity might be large (pixels/sec), so we might want to scale it down or just take direction.
            // The requirement says "horizontal bias based on the paddle's velocity".
            
            // Let's normalize paddle velocity effect relative to speed to get a ratio, or just clamp it.
            // If paddle moves 400px/s and biasFactor is e.g. 0.001, effect is small.
            // If we treat biasFactor as "how much of paddle velocity is added to the launch vector (which is normalized 0-1)?"
            // That might be tricky if unit scales differ.
            
            // Simpler approach for TDD:
            // Direction = (0, -1) + (biasX, 0). 
            // If biasX is derived directly from velocity, we need to know the scale.
            // Let's assume biasFactor accounts for the scale.
            
            Vector2 launchDir = new Vector2(biasX / speed, -1).Normalized();
            return launchDir * speed;
        }
    }
}

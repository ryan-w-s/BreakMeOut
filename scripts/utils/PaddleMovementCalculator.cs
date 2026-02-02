using System;

namespace BreakMeOut.Scripts.Utils
{
    public static class PaddleMovementCalculator
    {
        public static float UpdatePosition(
            float currentX,
            float mouseX,
            float lastMouseX,
            bool isLeftPressed,
            bool isRightPressed,
            float speed,
            float delta,
            float minX,
            float maxX
        )
        {
            float nextX = currentX;
            // Check if mouse has moved significantly
            bool mouseMoved = Math.Abs(mouseX - lastMouseX) > 0.001f;

            if (mouseMoved)
            {
                nextX = mouseX;
            }
            else
            {
                if (isLeftPressed)
                {
                    nextX -= speed * delta;
                }
                if (isRightPressed)
                {
                    nextX += speed * delta;
                }
            }

            return Math.Clamp(nextX, minX, maxX);
        }
    }
}
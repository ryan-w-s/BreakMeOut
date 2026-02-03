using Godot;

namespace BreakMeOut.Actors
{
    /// <summary>
    /// Simple visual component that draws a colored circle for the ball.
    /// </summary>
    public partial class BallVisuals : Node2D
    {
        /// <summary>
        /// The radius of the ball to draw.
        /// </summary>
        [Export]
        public float Radius { get; set; } = 15.0f;

        /// <summary>
        /// The color of the ball.
        /// </summary>
        [Export]
        public Color Color { get; set; } = new Color(1.0f, 0.8f, 0.2f); // Golden/yellow color

        public override void _Draw()
        {
            // Draw a filled circle centered at (0, 0)
            DrawCircle(Vector2.Zero, Radius, Color);
        }
    }
}

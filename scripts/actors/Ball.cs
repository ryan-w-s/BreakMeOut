using Godot;
using System;
using BreakMeOut.Scripts.Utils;

public partial class Ball : CharacterBody2D
{
	[Export] public float Speed = 500.0f;
	[Export] public Vector2 HeldOffset = new Vector2(0, -30);
	[Export] public float LaunchBiasFactor = 0.5f;
	
	// Initial direction - will be randomized in game logic
	public Vector2 Direction = new Vector2(0.5f, -1.0f).Normalized();
	
	public bool IsHeld { get; private set; } = false;
	private Paddle _paddle;

	public override void _PhysicsProcess(double delta)
	{
		if (IsHeld)
		{
			if (_paddle != null && IsInstanceValid(_paddle))
			{
				Position = BallStateCalculator.CalculateHeldPosition(_paddle.Position, HeldOffset);
			}

			// Launch Input Check
			if (Input.IsActionJustPressed("ui_accept") || Input.IsMouseButtonPressed(MouseButton.Left))
			{
				Launch();
			}
		}
		else
		{
			Velocity = Direction * Speed;
			KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

			if (collision != null)
			{
				// Check what we hit
				if (collision.GetCollider() is Brick brick)
				{
					// Standard Bounce for bricks
					Direction = Direction.Bounce(collision.GetNormal());
					brick.Hit();
				}
				else if (collision.GetCollider() is Paddle paddle)
				{
					// Dynamic Bounce for paddle
					Vector2 paddleVel = paddle.GetCurrentVelocity();
					Direction = BallStateCalculator.CalculateBounceDirection(
						Direction,
						collision.GetNormal(),
						paddleVel,
						LaunchBiasFactor, // Reusing same factor for now
						Speed
					);
				}
				else
				{
					// Standard Bounce for everything else (walls)
					Direction = Direction.Bounce(collision.GetNormal());
				}
			}
		}
	}

	public void AttachToPaddle(Paddle paddle)
	{
		_paddle = paddle;
		IsHeld = true;
	}

	public void Launch()
	{
		if (!IsHeld) return;
		
		Vector2 paddleVel = Vector2.Zero;
		if (_paddle != null && IsInstanceValid(_paddle))
		{
			paddleVel = _paddle.GetCurrentVelocity();
		}

		Vector2 launchVel = BallStateCalculator.CalculateLaunchVelocity(Speed, paddleVel, LaunchBiasFactor);
		
		// Set Direction based on calculated velocity
		Direction = launchVel.Normalized();
		
		IsHeld = false;
	}
}

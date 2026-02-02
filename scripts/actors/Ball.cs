using Godot;
using System;
using BreakMeOut.Scripts.Utils;

public partial class Ball : CharacterBody2D
{
	[Export] public float Speed = 500.0f;
	[Export] public Vector2 HeldOffset = new Vector2(0, -30);
	
	// Initial direction - will be randomized in game logic
	public Vector2 Direction = new Vector2(0.5f, -1.0f).Normalized();
	
	public bool IsHeld { get; private set; } = true;
	private Paddle _paddle;

	public override void _PhysicsProcess(double delta)
	{
		if (IsHeld)
		{
			if (_paddle != null && IsInstanceValid(_paddle))
			{
				Position = BallStateCalculator.CalculateHeldPosition(_paddle.Position, HeldOffset);
			}
		}
		else
		{
			Velocity = Direction * Speed;
			KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

			if (collision != null)
			{
				// Bounce
				Direction = Direction.Bounce(collision.GetNormal());
				
				// Check what we hit
				if (collision.GetCollider() is Brick brick)
				{
					brick.Hit();
				}
				else if (collision.GetCollider() is Paddle paddle)
				{
					// Optional: Adjust angle based on paddle hit position
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
		IsHeld = false;
		// Launch logic will be refined in next task
		Direction = Vector2.Up;
	}
}

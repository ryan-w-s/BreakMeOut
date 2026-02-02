using Godot;
using System;
using BreakMeOut.Scripts.Utils;

public partial class Paddle : CharacterBody2D
{
	[Export] public float Speed = 400.0f;

	private float _halfWidth;
	private float _lastMouseX;

	public override void _Ready()
	{
		var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
		if (collisionShape.Shape is RectangleShape2D rect)
		{
			_halfWidth = rect.Size.X / 2;
		}
		else
		{
			// Fallback if shape is different or not found
			_halfWidth = 60.0f; 
		}

		_lastMouseX = GetGlobalMousePosition().X;
	}

	public override void _PhysicsProcess(double delta)
	{
		float currentX = Position.X;
		float mouseX = GetGlobalMousePosition().X;
		bool left = Input.IsActionPressed("ui_left");
		bool right = Input.IsActionPressed("ui_right");
		float dt = (float)delta;
		Rect2 viewport = GetViewportRect();

		// Calculate Clamping Bounds
		float minX = _halfWidth;
		float maxX = viewport.Size.X - _halfWidth;

		// Calculate Target Position
		float targetX = PaddleMovementCalculator.UpdatePosition(
			currentX,
			mouseX,
			_lastMouseX,
			left,
			right,
			Speed,
			dt,
			minX,
			maxX
		);

		// Move via Velocity to respect physics engine (collisions)
		// We want to reach targetX in exactly 'dt' time.
		float velX = PaddleMovementCalculator.CalculateVelocity(currentX, targetX, dt);
		Velocity = new Vector2(velX, 0);

		MoveAndSlide();

		_lastMouseX = mouseX;
	}

	/// <summary>
	/// Returns the current velocity of the paddle.
	/// Used by the Ball to calculate bounce angles.
	/// </summary>
	public Vector2 GetCurrentVelocity()
	{
		return Velocity;
	}
}

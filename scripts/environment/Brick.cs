using Godot;
using System;

public partial class Brick : StaticBody2D
{
	[Signal] public delegate void BrickDestroyedEventHandler(Vector2 position);

	[Export] public PackedScene BallScene;

	[Export] public int Health = 1;

	private Label _healthLabel;
	private ColorRect _colorRect;

	public override void _Ready()
	{
		_healthLabel = GetNodeOrNull<Label>("HealthLabel");
		_colorRect = GetNodeOrNull<ColorRect>("ColorRect");
		UpdateHealthDisplay();
		UpdateColor();
		AddToGroup("bricks");
		GetNode<GameManager>("/root/GameManager").RegisterBrick();
	}

	public void SetHealth(int health)
	{
		Health = health;
		if (IsInsideTree())
		{
			UpdateHealthDisplay();
			UpdateColor();
		}
	}

	public void Hit()
	{
		Health--;
		UpdateHealthDisplay();
		UpdateColor();

		if (Health <= 0)
		{
			// Remove from group before checking victory to prevent race conditions
			RemoveFromGroup("bricks");

			EmitSignal(SignalName.BrickDestroyed, GlobalPosition);

			// Spawn new ball
			if (BallScene != null)
			{
				var ball = BallScene.Instantiate<Node2D>();
				ball.GlobalPosition = GlobalPosition;
				GetTree().CurrentScene.CallDeferred("add_child", ball);
			}

			var gm = GetNode<GameManager>("/root/GameManager");
			gm.AddScore(100);
			gm.BrickDestroyed();
			QueueFree();
		}
	}

	private void UpdateHealthDisplay()
	{
		if (_healthLabel != null)
		{
			_healthLabel.Text = Health.ToString();
		}
	}

	private void UpdateColor()
	{
		if (_colorRect != null)
		{
			_colorRect.Color = Health switch
			{
				1 => new Color(0.2f, 0.8f, 0.2f), // Green
				2 => new Color(0.2f, 0.6f, 1f),  // Blue
				3 => new Color(1f, 1f, 0.2f),    // Yellow
				4 => new Color(1f, 0.6f, 0.2f),  // Orange
				_ => new Color(1f, 0.2f, 0.2f)   // Red
			};
		}
	}
}
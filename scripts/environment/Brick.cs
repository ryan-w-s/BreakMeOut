using Godot;
using System;

public partial class Brick : StaticBody2D
{
	[Signal] public delegate void BrickDestroyedEventHandler(Vector2 position);

	[Export] public PackedScene BallScene;

	[Export] public int Health = 1;

	private Label _healthLabel;

	public override void _Ready()
	{
		_healthLabel = GetNode<Label>("HealthLabel");
		UpdateHealthDisplay();
		GetNode<GameManager>("/root/GameManager").RegisterBrick();
	}

	public void Hit()
	{
		Health--;
		UpdateHealthDisplay();
		
		if (Health <= 0)
		{
			EmitSignal(SignalName.BrickDestroyed, GlobalPosition);
			
			// Spawn new ball
			if (BallScene != null)
			{
				var ball = BallScene.Instantiate<Node2D>();
				ball.GlobalPosition = GlobalPosition;
				// Add to main scene root or a container if possible
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
}

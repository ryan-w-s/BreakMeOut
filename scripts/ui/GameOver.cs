using Godot;
using System;

public partial class GameOver : Control
{
    public override void _Ready()
    {
        GetNode<Button>("VBoxContainer/RestartButton").Pressed += OnRestartPressed;
        GetNode<Button>("VBoxContainer/MenuButton").Pressed += OnMenuPressed;
    }

    private void OnRestartPressed()
    {
        GetNode<GameManager>("/root/GameManager").ResetGame();
        GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
    }

    private void OnMenuPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/ui/MainMenu.tscn");
    }
}

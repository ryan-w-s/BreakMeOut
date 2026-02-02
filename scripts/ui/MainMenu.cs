using Godot;
using System;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
		GD.Print("MainMenu _Ready");
		var startBtn = GetNode<Button>("VBoxContainer/StartButton");
		var selectBtn = GetNode<Button>("VBoxContainer/SelectLevelButton");
		var quitBtn = GetNode<Button>("VBoxContainer/QuitButton");
		
		if (startBtn == null) GD.PrintErr("StartButton not found!");
		else startBtn.Pressed += OnStartPressed;

		if (selectBtn == null) GD.PrintErr("SelectLevelButton not found!");
		else selectBtn.Pressed += OnSelectLevelPressed;

		if (quitBtn == null) GD.PrintErr("QuitButton not found!");
		else quitBtn.Pressed += OnQuitPressed;
	}

	private void OnStartPressed()
	{
		GD.Print("Start Pressed");
		GetNode<GameManager>("/root/GameManager").ResetGame();
		var result = GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
		GD.Print($"ChangeSceneToFile result: {result}");
	}

	private void OnSelectLevelPressed()
	{
		GD.Print("Select Level Pressed");
		GetTree().ChangeSceneToFile("res://scenes/ui/LevelSelect.tscn");
	}

	private void OnQuitPressed()
	{
		GD.Print("Quit Pressed");
		GetTree().Quit();
	}
}

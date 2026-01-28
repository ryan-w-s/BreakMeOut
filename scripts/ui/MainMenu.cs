using Godot;
using System;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        GD.Print("MainMenu _Ready");
        var startBtn = GetNode<Button>("VBoxContainer/StartButton");
        var quitBtn = GetNode<Button>("VBoxContainer/QuitButton");
        
        if (startBtn == null) GD.PrintErr("StartButton not found!");
        else startBtn.Pressed += OnStartPressed;

        if (quitBtn == null) GD.PrintErr("QuitButton not found!");
        else quitBtn.Pressed += OnQuitPressed;
    }

    private void OnStartPressed()
    {
        GD.Print("Start Pressed");
        var result = GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
        GD.Print($"ChangeSceneToFile result: {result}");
    }

    private void OnQuitPressed()
    {
        GD.Print("Quit Pressed");
        GetTree().Quit();
    }
}

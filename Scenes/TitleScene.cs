using Godot;
using System;

public partial class TitleScene : Node2D
{
	private Button startButton;

	public override void _Ready()
	{
		startButton = GetNode<Button>("StartButton");
		
		startButton.Pressed += OnStartButtonPressed;
	}

	private void OnStartButtonPressed()
	{
		GD.Print("Start Game button pressed!");

		GetTree().ChangeSceneToFile("res://Scenes/choosing_ingredients.tscn");
	}
}

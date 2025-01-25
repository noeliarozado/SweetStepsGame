using Godot;
using System;

public partial class TitleScene : Node2D
{
	private Button startButton;

	public override void _Ready()
	{
		// Get the reference to the "Start Game" button
		startButton = GetNode<Button>("StartButton");

		// Connect the button's pressed signal to the handler function
		startButton.Pressed += OnStartButtonPressed;
	}

	// This function is called when the Start Game button is pressed
	private void OnStartButtonPressed()
	{
		GD.Print("Start Game button pressed!");

		// Change the scene to the Ingredients scene
		GetTree().ChangeSceneToFile("res://Scenes/choosing_ingredients.tscn");  // Adjust this path if needed
	}
}

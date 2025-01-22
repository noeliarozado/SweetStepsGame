using Godot;
using System;

public partial class ChoosingIngredients : Node2D
{
	private int selectedIngredients = 0;

	private Button flourButton;
	private Button eggButton;
	private Button butterButton;
	private Button sugarButton;

	public override void _Ready()
	{
		flourButton = GetNode<Button>("FlourButton");
		eggButton = GetNode<Button>("EggButton");
		butterButton = GetNode<Button>("ButterButton");
		sugarButton = GetNode<Button>("SugarButton");

		flourButton.Pressed += () => OnIngredientSelected(flourButton);
		eggButton.Pressed += () => OnIngredientSelected(eggButton);
		butterButton.Pressed += () => OnIngredientSelected(butterButton);
		sugarButton.Pressed += () => OnIngredientSelected(sugarButton);
	}

	private void OnIngredientSelected(Button selectedButton)
	{
		// Hide the button after it is clicked
		selectedButton.Visible = false;

		selectedIngredients++;

		if (selectedIngredients == 4)
		{
			GetTree().ChangeSceneToFile("res://Scenes/mixing_ingredients.tscn");
		}
	}
}

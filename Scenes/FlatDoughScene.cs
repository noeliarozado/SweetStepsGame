using Godot;
using System;

public partial class FlatDoughScene : Node2D
{
	private Label messageLabel; 
	private Sprite2D dough;   
	private Sprite2D starBiscuit; 
	private bool biscuitChosen = false; 

	public override void _Ready()
	{
		messageLabel = GetNode<Label>("MessageLabel");
		dough = GetNode<Sprite2D>("Dough");
		starBiscuit = GetNode<Sprite2D>("StarBiscuit");

		starBiscuit.Visible = false;

		Button starMoldButton = GetNode<Button>("StarMold");
		starMoldButton.Pressed += OnStarMoldPressed;
	}

	private void OnStarMoldPressed()
	{
		if (!biscuitChosen)
		{
			dough.Visible = false;
			starBiscuit.Visible = true;
			
			starBiscuit.Position += new Vector2(-50, 0);  // Moves the sprite 50 pixels to the left

			biscuitChosen = true; 

			TransitionToBakingScene();
		}
		else
		{
			GD.Print("Biscuit already chosen, ignoring additional clicks.");
		}
	}

	private void TransitionToBakingScene()
	{
		// Delay the transition for 4 seconds
		GetTree().CreateTimer(4.0f).Timeout += () =>
		{
			GD.Print("Creating new Baking scene...");

			string scenePath = "res://Scenes/baking_scene.tscn";
			if (ResourceLoader.Exists(scenePath))
			{
				GD.Print("Baking scene path exists.");
				PackedScene bakingScene = GD.Load<PackedScene>(scenePath);

				if (bakingScene != null)
				{
					GD.Print("Baking scene loaded successfully.");
					Node bakingInstance = bakingScene.Instantiate();

					// Add the baking scene to the scene tree
					GetTree().Root.AddChild(bakingInstance);
					QueueFree(); // Remove the current scene
				}
				else
				{
					GD.PrintErr("Failed to load Baking scene.");
				}
			}
			else
			{
				GD.PrintErr($"The scene at '{scenePath}' does not exist.");
			}
		};
	}
}

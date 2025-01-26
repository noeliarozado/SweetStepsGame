using Godot;
using System;

public partial class MixingIngredients : Node2D
{
	private ProgressBar mixProgressBar;
	private Label messageLabel;
	private Button mixButton;

	private float progressSpeed = 50f;
	private bool isMixing = false;

	public override void _Ready()
	{
		mixProgressBar = GetNode<ProgressBar>("MixProgressBar");
		messageLabel = GetNode<Label>("MessageLabel");
		mixButton = GetNode<Button>("MixButton");
		
		mixProgressBar.Modulate = new Color(255 / 255f, 215 / 255f, 0 / 255f);

		mixButton.Pressed += OnMixButtonPressed;
	}

	private void OnMixButtonPressed()
	{
		if (!isMixing)
		{
			isMixing = true;
			mixButton.Disabled = true;
		}
	}

	public override void _Process(double delta)
	{
		if (isMixing)
		{
			// Increase the progress bar value
			mixProgressBar.Value += progressSpeed * (float)delta;

			// Check if mixing is complete
			if (mixProgressBar.Value >= mixProgressBar.MaxValue)
			{
				isMixing = false;
				mixProgressBar.Value = mixProgressBar.MaxValue;

				// Wait for 1 second and change to the new scene
				Timer timer = new Timer();
				AddChild(timer);
				timer.WaitTime = 1.0f;
				timer.OneShot = true;
				timer.Timeout += () =>
				{
					GetTree().ChangeSceneToFile("res://Scenes/dough_scene.tscn");
				};
				timer.Start();
			}
		}
	}
}

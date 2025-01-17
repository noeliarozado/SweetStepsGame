using Godot;
using System;

public partial class Dough : Sprite2D
{
	private float kneadingTime = 0f; // Time spent kneading
	private const float KneadingThreshold = 5f; // Time required to finish kneading
	private ProgressBar progressBar; // Progress bar for kneading progress
	private bool isKneaded = false; // Flag to prevent multiple changes
	private Texture2D flatDoughTexture; // Texture for the flat dough

	public override void _Ready()
	{
		// Get the ProgressBar node (make sure it exists under Dough)
		progressBar = GetNode<ProgressBar>("ProgressBar");
		progressBar.MaxValue = KneadingThreshold; // Set the maximum value for the bar
		progressBar.Value = 0; // Start with no progress

		// Load the flat dough texture
		flatDoughTexture = (Texture2D)GD.Load("res://flatdough.png");
	}

	public void AddKneadingTime(float delta)
	{
		if (isKneaded) return; // Do nothing if already kneaded

		// Increase kneading time
		kneadingTime += delta;
		progressBar.Value = kneadingTime;

		// Check if the dough is fully kneaded
		if (kneadingTime >= KneadingThreshold)
		{
			KneadComplete();
		}
	}

private void KneadComplete()
{
	// Change the texture to the flat dough
	Texture = flatDoughTexture;

	// Optional: Adjust the size (make it slightly larger for flat dough)
	Scale *= 1.25f; // Increase the scale by 20%

	// Optional: Print a message or trigger further gameplay
	GD.Print("Dough is now flat!");

	// Mark as kneaded
	isKneaded = true;

	// Optional: Disable the ProgressBar
	progressBar.Visible = false;

	// You can add further interactions here, such as enabling tools or progressing to the next step
}


	public Vector2 GetDoughSize()
	{
		return Texture.GetSize() * Scale; // Access the texture size directly from the Sprite2D
	}

	public Vector2 GetDoughPosition()
	{
		return GlobalPosition; // Access the global position of the Sprite2D
	}
}

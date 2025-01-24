using Godot;
using System;

public partial class BakingScene : Node2D
{
	private Sprite2D starTray;      
	private ProgressBar progressBar; 
	private Timer bakingTimer;    
	private bool isBaking = false;  

	public override void _Ready()
	{
		starTray = GetNode<Sprite2D>("StarTray");
		progressBar = GetNode<ProgressBar>("BakingProgressBar");
		bakingTimer = new Timer();
		AddChild(bakingTimer);

		starTray.Visible = true;

		GetNode<Button>("StartBakingButton").Pressed += OnStartBakingPressed;
	}

	private void OnStartBakingPressed()
	{
		if (!isBaking)
		{
			isBaking = true;

			progressBar.Value = 0;
			bakingTimer.WaitTime = 0.1f; 
			bakingTimer.OneShot = false;
			bakingTimer.Timeout += () =>
			{
				progressBar.Value += 10;

				// Check if baking is done
				if (progressBar.Value >= progressBar.MaxValue)
				{
					bakingTimer.Stop();
					TransitionToDecorationScene();
				}
			};
			bakingTimer.Start();
		}
	}

	private void TransitionToDecorationScene()
	{
		string scenePath = "res://Scenes/decoration_scene.tscn";
		if (ResourceLoader.Exists(scenePath))
		{
			PackedScene decorationScene = GD.Load<PackedScene>(scenePath);
			if (decorationScene != null)
			{
				Node decorationInstance = decorationScene.Instantiate();
				GetTree().Root.AddChild(decorationInstance);
				QueueFree();
			}
			else
			{
				GD.PrintErr("Failed to load Decoration Scene.");
			}
		}
		else
		{
			GD.PrintErr($"The scene at '{scenePath}' does not exist.");
		}
	}
}

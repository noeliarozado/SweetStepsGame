using Godot;
using System;

public partial class DoughScene : Node2D
{
	private bool isDragging = false;  
	private Sprite2D rollingPin;    
	private Sprite2D dough;        
	private ProgressBar progressBar; 

	private float previousYPosition; 
	private bool movedUp = false;   
	private bool movedDown = false; 

	private float kneadingProgress = 0; 

	public override void _Ready()
	{
		rollingPin = GetNode<Sprite2D>("RollingPin");
		dough = GetNode<Sprite2D>("Dough");
		progressBar = GetNode<ProgressBar>("RollingProgressBar");
		
		progressBar.Modulate = new Color(255 / 255f, 215 / 255f, 0 / 255f);
	
		progressBar.Value = kneadingProgress;

		previousYPosition = rollingPin.GlobalPosition.Y;
	}

	public override void _Process(double delta)
	{
		// Move the rolling pin with the mouse while dragging
		if (isDragging)
		{
			rollingPin.GlobalPosition = GetViewport().GetMousePosition();

			// Check if the rolling pin has moved up or down
			float currentYPosition = rollingPin.GlobalPosition.Y;

			if (currentYPosition < previousYPosition - 10) // Moved up by at least 10 pixels
			{
				movedUp = true;
			}
			else if (currentYPosition > previousYPosition + 10) // Moved down by at least 10 pixels
			{
				movedDown = true;
			}

			previousYPosition = currentYPosition; 

			if (movedUp && movedDown)
			{
				movedUp = false;
				movedDown = false;

				// Check if the rolling pin overlaps the dough
				Rect2 rollingPinRect = new Rect2(
					rollingPin.GlobalPosition - (rollingPin.Texture.GetSize() * rollingPin.Scale / 2),
					rollingPin.Texture.GetSize() * rollingPin.Scale
				);

				Rect2 doughRect = new Rect2(
					dough.GlobalPosition - (dough.Texture.GetSize() * dough.Scale / 2),
					dough.Texture.GetSize() * dough.Scale
				);

				if (rollingPinRect.Intersects(doughRect))
				{
					kneadingProgress += (float)delta * 50; 
					progressBar.Value = kneadingProgress;

					if (kneadingProgress >= 100)
					{
						GD.Print("Dough is ready!");
						GetTree().ChangeSceneToFile("res://Scenes/flat_dough_scene.tscn");
					}
				}
			}
		}
	}

	public override void _Input(InputEvent @event)
	{
		// Start dragging when the left mouse button is pressed
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			Vector2 mousePosition = GetViewport().GetMousePosition();

			Rect2 rollingPinRect = new Rect2(
				rollingPin.GlobalPosition - (rollingPin.Texture.GetSize() * rollingPin.Scale / 2),
				rollingPin.Texture.GetSize() * rollingPin.Scale
			);

			if (rollingPinRect.HasPoint(mousePosition))
			{
				isDragging = true;
			}
		}

		// Stop dragging when the left mouse button is released
		if (@event is InputEventMouseButton mouseEventRelease && !mouseEventRelease.Pressed && mouseEventRelease.ButtonIndex == MouseButton.Left)
		{
			isDragging = false;
		}
	}
}

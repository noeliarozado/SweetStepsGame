using Godot;
using System;

public partial class Game : Node2D
{
	private bool isDragging = false;  // Is the rolling pin being dragged?
	private Sprite2D rollingPin;     // Rolling pin node
	private Dough dough;             // Dough node reference

	private float previousYPosition; // Store the previous Y position of the rolling pin
	private bool movedUp = false;    // Track if the rolling pin has moved up
	private bool movedDown = false;  // Track if the rolling pin has moved down

	public override void _Ready()
	{
		// Get node references
		rollingPin = GetNode<Sprite2D>("RollingPin");
		dough = GetNode<Dough>("Dough");

		// Initialize the previous Y position
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

			previousYPosition = currentYPosition; // Update the previous Y position

			// Check if both up and down movements have occurred
			if (movedUp && movedDown)
			{
				// Reset movement flags
				movedUp = false;
				movedDown = false;

				// Check if the rolling pin overlaps the dough
				Rect2 rollingPinRect = new Rect2(
					rollingPin.GlobalPosition - (rollingPin.Texture.GetSize() * rollingPin.Scale / 2),
					rollingPin.Texture.GetSize() * rollingPin.Scale
				);

				Rect2 doughRect = new Rect2(
					dough.GetDoughPosition() - (dough.GetDoughSize() / 2),
					dough.GetDoughSize()
				);

				if (rollingPinRect.Intersects(doughRect))
				{
					dough.AddKneadingTime((float)delta  * 7); // Add kneading time
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

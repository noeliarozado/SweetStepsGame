using Godot;
using System;

public partial class DecorationScene : Node2D
{
	private Sprite2D starTray;   
	private Button decoratingButton; 
	private Button eatCookiesButton; 

	public override void _Ready()
	{
		starTray = GetNode<Sprite2D>("StarTray");
		decoratingButton = GetNode<Button>("DecoratingButton");
		eatCookiesButton = GetNode<Button>("EatCookiesButton");

		eatCookiesButton.Visible = false;

		decoratingButton.Pressed += OnDecoratingButtonPressed;
		eatCookiesButton.Pressed += OnEatCookiesButtonPressed;
	}

	private void OnDecoratingButtonPressed()
	{
		starTray.Texture = GD.Load<Texture2D>("res://Images/icedcookies.png");
		
		starTray.Scale = new Vector2(0.75f, 0.75f);
		starTray.Position = new Vector2(400, 270);
		
		decoratingButton.Visible = false;
		eatCookiesButton.Visible = true;
	}

	private void OnEatCookiesButtonPressed()
	{
		GD.Print("Eating cookies... Goodbye!");
		GetTree().Quit();
	}
}

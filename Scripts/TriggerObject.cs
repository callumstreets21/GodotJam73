using Godot;
using System;

public partial class TriggerObject : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Connect the signals for the Area3D
		this.BodyEntered += OnBodyEntered;
		this.BodyExited += OnBodyExited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnBodyEntered(Node body)
	{
		GD.Print( body.Name +"just entered.");
	}
	
	private void OnBodyExited(Node body)
	{
		GD.Print( body.Name +"just exited.");
	}
}

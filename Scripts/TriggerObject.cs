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

	protected virtual void OnBodyEntered(Node body)
	{
		GD.Print( body.Name +"just entered.");
	}
	
	protected virtual void OnBodyExited(Node body)
	{
		GD.Print( body.Name +"just exited.");
	}
}

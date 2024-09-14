using Godot;
using System;

public partial class Interactable : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public void Use()
	{
		GD.Print("interacted with " + this.Name);
	}
	
}

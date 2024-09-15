using Godot;
using System;

public partial class Interactable : Area3D
{
	// Call this to interact with the object, needs to be overriden in child classes
	public void Use()
	{
		GD.Print("interacted with " + this.Name);
	}
	
}

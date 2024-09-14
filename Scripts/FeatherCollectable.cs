using Godot;
using System;

public partial class FeatherCollectable : TriggerObject
{
	protected override void OnBodyEntered(Node body)
	{
		if (body is CharacterBody3D)
		{
			CollectableManager.Instance.CollectFeather();
			this.QueueFree();
		}
		
	}
}

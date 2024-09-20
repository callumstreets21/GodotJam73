using Godot;
using System;

namespace GodotTemplate.Scripts;
public partial class Activator : Node3D
{
	[Export] DynamicObject targetObject;
	
	public virtual void Use()
	{
		if (targetObject == null)
		{
			GD.PrintErr($"No Target Object set on {Name}, child of {GetParent().Name}");
			return;
		}
		
		GD.Print($"{Name} was used.");
		targetObject.Activate();
	}
}

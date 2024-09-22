using Godot;
using System;

namespace GodotTemplate.Scripts;
public partial class Activator : Node3D
{
	[Export] DynamicObject targetObject;
	[Export] private AudioStream activateSound;
	
	public virtual void Use()
	{
		if (targetObject == null)
		{
			GD.PrintErr($"No Target Object set on {Name}, child of {GetParent().Name}");
			return;
		}
		
		GD.Print($"{Name} was used.");
		if (activateSound != null) AudioManager.Instance.PlayClip(activateSound, this.Position);
		targetObject.Activate();
	}
}

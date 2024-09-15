using Godot;
using System;

namespace GodotTemplate.Scripts;

public partial class DamageComponent : Area3D
{
	[Export] private float damageAmount;

	public enum DamageEntity
	{
		Player = 1 << 0,
		Enemy = 1 << 1
	}

	[Export] public DamageEntity activeLayer = DamageEntity.Player;
	
	public override void _Ready()
	{
		CollisionMask = (uint)activeLayer;
	}
	
	protected virtual void OnBodyEntered(Node body)
	{
		if (body != null)
		{
			var healthComponent = body.GetNode<HealthComponent>("HealthComponent");
			if (healthComponent != null)
			{
				healthComponent.TakeDamage(damageAmount);
			}
		}
	}
}

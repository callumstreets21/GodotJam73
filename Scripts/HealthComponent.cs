using Godot;
using System;

namespace GodotTemplate.Scripts;

public partial class HealthComponent : Node3D
{
	[Export]
	private float health;
	[Export]
	private bool isInvincible;
	[Export] 
	private float maxHealth;
	private bool isDead;
	private Node3D parent_class;
	
	public float Health
	{
		get { return health; }
		set { health = value;
			if (health <= 0)
			{
				Die();
			}
		}
	}
	
	public override void _Ready()
	{
		base._Ready();
		health = maxHealth;
		parent_class = GetParent<Node3D>();
	}
	
	public void TakeDamage(float damage)
	{
		if (!isInvincible)
		{
			health -= damage;
			if (health <= 0)
			{
				Die();
			}
		}
	}
	
	public void Heal(float healAmount)
	{
		health += healAmount;
		if (health > maxHealth)
		{
			health = maxHealth;
		}
	}
	
	public virtual void Die()
	{
		GetNode("/root/LevelManager").Call("reload_scene");
	}
	
	public void SetInvincible(bool invincible)
	{
		isInvincible = invincible;
	}
}

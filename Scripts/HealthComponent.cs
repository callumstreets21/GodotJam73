using Godot;
using System;

namespace GodotTemplate.Scripts;

public partial class HealthComponent : Node3D
{
    private float health;
    private bool isInvincible;
    private bool isDead;
    private Node3D parent_class;
    
    public override void _Ready()
    {
        health = 100.0f;
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
        if (health > 100)
        {
            health = 100;
        }
    }
    
    public void Die()
    {
        isDead = true;
        health = 0;
        PackedScene gameOverScene = (PackedScene)ResourceLoader.Load("res://Scenes/_menus/game_over_screen.tscn");
        Node gameOverInstance = gameOverScene.Instantiate();
        GetTree().Root.AddChild(gameOverInstance);
    }
    
    public void Respawn()
    {
        isDead = false;
        health = 100;
        // Reload the current level
        GetTree().ReloadCurrentScene();
    }
    
    public void SetInvincible(bool invincible)
    {
        isInvincible = invincible;
    }
    
    public float GetHealth()
    {
        return health;
    }
    
    public void SetHealth(float newHealth)
    {
        health = newHealth;
    }
    
}


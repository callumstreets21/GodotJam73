using Godot;
using System;

namespace GodotTemplate.Scripts;

public partial class ActivatorPressurePad : Activator
{
     
    
    // Configuration
    [Export] private bool triggerOnExit = false;
    [Export] float depressionDistance = 0.05f;

    // Cached references
    private MeshInstance3D mesh;
    public override void _Ready()
    {
        base._Ready();
        mesh = Utils.FindComponentInChildren<MeshInstance3D>(this);
    }

    public void _on_area_3d_body_entered(Node other)
    {
        if (!other.IsInGroup("Player")) return;
        
        mesh.Position -= new Vector3(0, depressionDistance, 0);
        Use();
    }

    public void _on_area_3d_body_exited(Node other)
    {
        if (!other.IsInGroup("Player")) return;
        
        mesh.Position += new Vector3(0, depressionDistance, 0);
        if (!triggerOnExit) return;
        Use();
    }
}
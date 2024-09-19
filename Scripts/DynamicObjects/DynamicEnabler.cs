using Godot;
using System;

namespace GodotTemplate.Scripts;

public partial class DynamicEnabler : DynamicObject
{
    [Export] private NodePath[] toggleTargets; // Use NodePath to reference nodes from the editor
    [Export] private bool startDisabled = true;

    public override void _Ready()
    {
        base._Ready();
        
        foreach (var nodePath in toggleTargets)
        {
            var node = GetNode(nodePath);  // Retrieve the node using its NodePath
            
            if (node is Node3D node3D)
            {
                node3D.Visible = !startDisabled;  // Toggle visibility for Node3D
            }
            if (node is CollisionShape3D collisionNode)
            {
                collisionNode.Disabled = !startDisabled;  // Toggle disabled state for CollisionShape3D
            }
            if (node is Area3D areaNode)
            {
                areaNode.Monitoring = !startDisabled;
            }
            // Add additional checks here for other types of nodes and their respective properties
        }
    }

    public override void Activate()
    {
        if (onceOnly && hasActivated) return;
        if (!reversible && hasActivated) return;
        base.Activate();

        ToggleStates();
    }

    private void ToggleStates()
    {
        foreach (var nodePath in toggleTargets)
        {
            var node = GetNode(nodePath);  // Retrieve the node using its NodePath
            if (node is Node3D node3D)
            {
                node3D.Visible = !node3D.Visible;  // Toggle visibility for Node3D
            }
            if (node is CollisionShape3D collisionNode)
            {
                collisionNode.Disabled = !collisionNode.Disabled;  // Toggle disabled state for CollisionShape3D
            }
            if (node is Area3D areaNode)
            {
                areaNode.Monitoring = !areaNode.Monitoring;
            }
            // Add additional checks here for other types of nodes and their respective properties
        }
    }
}
using Godot;
using System;

namespace GodotTemplate.Scripts;
public partial class DynamicObject: Node3D
{
    [Export] protected bool onceOnly = true;
    [Export] protected bool reversible = false;

    protected bool hasActivated = false;
    protected bool directionReversed = true;    // Objects are moved in process so we default this to true so that one activation sets it to false.
    
    public virtual void Activate()
    {
        if (onceOnly && hasActivated) return;
        if (reversible) directionReversed = !directionReversed;
        
        hasActivated = true;
        GD.Print($"{Name} was activated.");
    }
}
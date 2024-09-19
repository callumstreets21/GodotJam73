using Godot;
using System;

namespace GodotTemplate.Scripts;
public partial class DynamicObject: Node3D
{
    public virtual void Activate()
    {
        GD.Print($"{Name} was activated.");
    }
}
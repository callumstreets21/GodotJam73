using Godot;
using System;

public partial class EndGoal : Node3D
{
	[Export] PackedScene next_level;
	public void _on_area_3d_body_entered(Node other)
	{
		if (!other.IsInGroup("Player")) return;
		
		GetNode("/root/LevelManager").Call("load_scene_by_path",next_level);
	}
}

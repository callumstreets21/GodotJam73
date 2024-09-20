using Godot;
namespace GodotTemplate.Scripts;


public partial class Teleporter : Node3D
{
	// Configuration
	[Export] Node3D teleportTarget;

	public void _on_area_3d_body_entered(Node other)
	{
		
		if (!other.IsInGroup("Player")) return;
		
		Node3D player = (Node3D)other;
		player.GlobalPosition = teleportTarget.GlobalPosition;
		player.GlobalRotation = teleportTarget.GlobalRotation;
		
	}
}

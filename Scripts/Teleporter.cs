using Godot;
namespace GodotTemplate.Scripts;


public partial class Teleporter : Node3D
{
	// Configuration
	[Export] Node3D teleportTarget;
	
	// Runtime variables
	private Vector3 teleportTargetPosition;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void _on_area_3d_body_entered(Node other)
	{
		if (!other.IsInGroup("Player")) return;
		
		Node3D player = (Node3D)other.GetParent();
			
		player.GlobalPosition = teleportTarget.GlobalPosition;
		player.GlobalRotation = teleportTarget.GlobalRotation;
	}
}

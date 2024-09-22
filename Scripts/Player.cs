using Godot;
namespace GodotTemplate.Scripts;
public partial class Player : CharacterBody3D
{
	private RayCast3D _cameraRay;

	public override void _Ready()
	{
		FindReferences();
	}

	private void FindReferences()
	{
		// Use the Utils class to find a RayCast3D component in children
		_cameraRay = Utils.FindComponentInChildren<RayCast3D>(this);

		if (_cameraRay != null)
		{
			_cameraRay.Enabled = true; // Enable the RayCast3D
			GD.Print("RayCast3D found and enabled!");
		}
		else
		{
			GD.Print("No RayCast3D found in the children of this node.");
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("primary_action"))
		{
			ActivateObject();
		}
	}

	private void ActivateObject()
	{
		_cameraRay.ForceRaycastUpdate();
		if (!_cameraRay.IsColliding()) return;
		
		// Check there is a collider
		var collider = _cameraRay.GetCollider() as Node;
		if (collider == null) return;

		// Check the collider i son an activator
		Activator activator = (Activator)collider.GetParent();
		if (activator == null) return;
		activator.Use();

	}
}

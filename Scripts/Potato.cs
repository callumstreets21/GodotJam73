using Godot;
using System;

public partial class Potato : CharacterBody3D
{
	[Export]
	public float Speed = 5;
	
	private NavigationAgent3D navAgent;
	private Vector3 targetPosition;

	public override void _Ready()
	{
		base._Ready();
		navAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");

		targetPosition = new Vector3(0, 0, 0);
		_UpdateTargetPosition(targetPosition);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		// Look at the target
		LookAt(targetPosition);
		
		// Keep the potato from looking at the floor
		var tempRotation = Rotation;
		tempRotation.Y = 0;
		tempRotation.Z = 0;
		Rotation = tempRotation;

		if (Position.DistanceTo(targetPosition) > 0.5)
		{
			Vector3 currentPosition = GlobalTransform.Origin;
			Vector3 nextPosition = navAgent.GetNextPathPosition();
			Vector3 nextVelocity = (nextPosition - currentPosition).Normalized() * Speed;
			targetPosition.Y = Position.Y;
			Velocity = nextVelocity;
			MoveAndSlide();
		}
	}

	private void _UpdateTargetPosition(Vector3 targetPosition)
	{
		navAgent.SetTargetPosition(targetPosition);
	}
}

using Godot;
using System;

namespace GodotTemplate.Scripts;

public partial class MovementComponent : Node3D
{
	private float speed;
	private Vector3 direction;
	private Node3D parent_to_move;
	public float mouseSensitivity = 0.1f; // public to allow for changing in the settings menu

	public override void _Ready()
	{
		speed = 10.0f;
		direction = Vector3.Zero;
		parent_to_move = GetParent<Node3D>();
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _PhysicsProcess(double delta)
	{
		direction = Vector3.Zero;
		if (Input.IsActionPressed("move_right"))
		{
			direction.X += 1;
		}
		if (Input.IsActionPressed("move_left"))
		{
			direction.X -= 1;
		}
		if (Input.IsActionPressed("move_down"))
		{
			direction.Z += 1;
		}
		if (Input.IsActionPressed("move_up"))
		{
			direction.Z -= 1;
		}

		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			direction = parent_to_move.Transform.Basis * direction;
		}

		//Vector3 newPosition = parent_to_move.GlobalTransform.Origin + direction * speed * (float)delta;
		//parent_to_move.GlobalTransform = new Transform3D(parent_to_move.GlobalTransform.Basis, newPosition);
		parent_to_move.GlobalPosition += direction * speed * (float)delta;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseEvent)
		{
			Vector3 rotation = parent_to_move.RotationDegrees;
			rotation.Y -= mouseEvent.Relative.X * mouseSensitivity;
			rotation.X -= mouseEvent.Relative.Y * mouseSensitivity;
			rotation.X = Mathf.Clamp(rotation.X, -90, 90);
			parent_to_move.RotationDegrees = rotation;
		}
	}
}

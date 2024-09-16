using Godot;
using System;

namespace GodotTemplate.Scripts;

public partial class MovementComponent : Node3D
{
	private float speed;
	private Vector3 direction;
	private Vector3 velocity;
	private Vector3 gravity;
	private CharacterBody3D parent_to_move;
	public float mouseSensitivity = 0.1f; // public to allow for changing in the settings menu
	[Export]
	private float jumpForce = 10.0f;
	private bool isOnGround = false;

	public override void _Ready()
	{
		speed = 10.0f;
		direction = Vector3.Zero;
		velocity = Vector3.Zero;
		parent_to_move = GetParent<CharacterBody3D>();
		Input.MouseMode = Input.MouseModeEnum.Captured;
		gravity = new Vector3(0, (float)ProjectSettings.GetSetting("physics/3d/default_gravity"), 0);
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
		
		isOnGround = parent_to_move.IsOnFloor();
		
		if (isOnGround && Input.IsActionJustPressed("jump"))
		{
			velocity.Y = jumpForce;
		}
		
		velocity -= gravity * (float)delta;
		velocity.X = direction.X * speed;
		velocity.Z = direction.Z * speed;
		
		parent_to_move.Velocity = velocity;
		parent_to_move.MoveAndSlide();
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

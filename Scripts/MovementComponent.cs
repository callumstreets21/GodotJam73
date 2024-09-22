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
		direction.X = Input.GetAxis("move_left", "move_right");
		direction.Z = Input.GetAxis("move_up", "move_down");
		
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

		LookByController(delta);
	}

	private void LookByController(double delta)
	{
		Vector2 inputVector = Input.GetVector("look_left", "look_right", "look_up", "look_down");
		Vector3 rotation = parent_to_move.RotationDegrees;
		rotation.Y -= inputVector.X * mouseSensitivity * (float)delta * 1080;	// TODO, evil magic numbers
		rotation.X -= inputVector.Y * mouseSensitivity * (float)delta * 1920;	// TODO, evil magic numbers
		rotation.X = Mathf.Clamp(rotation.X, -90, 90);
		parent_to_move.RotationDegrees = rotation;
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

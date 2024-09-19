using Godot;
using System;

namespace GodotTemplate.Scripts;

public partial class MovementComponent : Node3D
{
	public Camera3D cam;
	[Export] private float speed, acceleration = 60.0f, deceleration = 100.0f, AirDrag = 2.0f, AirAcceleration = 10.0f;
	private Vector2 InputDir;
	private Vector3 direction;
	private Vector3 velocity;
	private float gravity;
	private CharacterBody3D parent_to_move;
	public float mouseSensitivity = 0.1f; // public to allow for changing in the settings menu
	[Export]
	private float jumpForce = 10.0f;
	private bool isOnGround = false;

	public override void _Ready()
	{
		cam = GetParent().GetNode<Camera3D>("Camera3D");
		direction = Vector3.Zero;
		velocity = Vector3.Zero;
		parent_to_move = GetParent<CharacterBody3D>();
		Input.MouseMode = Input.MouseModeEnum.Captured;
		gravity = (float)ProjectSettings.GetSetting("physics/3d/default_gravity");
	}

	public override void _PhysicsProcess(double delta)
	{
		velocity = parent_to_move.GetRealVelocity();

		InputDir = Input.GetVector("move_left","move_right","move_down","move_up");
		isOnGround = parent_to_move.IsOnFloor();
		
		if (isOnGround && Input.IsActionJustPressed("jump"))
		{
			velocity.Y = jumpForce;
		}

		direction = parent_to_move.Transform.Basis * new Vector3(InputDir.X, 0, -InputDir.Y);

		Vector2 FlatVToward = new Vector2(velocity.X,velocity.Z);
		//move player velocity vector based on input
		FlatVToward = FlatVToward.MoveToward(new Vector2(direction.X,direction.Z) * speed, (InputDir.DistanceTo(Vector2.Zero) > 0 ? (isOnGround ? acceleration : AirAcceleration) : (isOnGround ? deceleration : AirDrag))*(float)delta);
		velocity = new Vector3(FlatVToward.X, velocity.Y, FlatVToward.Y);

		if(isOnGround){ //grounded
			
		}else{ //not grounded
			velocity.Y -= gravity * (float)delta;
		}		
		
		parent_to_move.Velocity = velocity;
		parent_to_move.MoveAndSlide();
		if(parent_to_move.IsOnFloor())
			parent_to_move.ApplyFloorSnap();
	}

	public override void _Input(InputEvent @event)
	{
		const float LockRot = 89.9f;
		if (@event is InputEventMouseMotion mouseEvent)
		{
			parent_to_move.RotateY(-mouseEvent.Relative.X * 0.005f * (float)GetNode("/root/OptionsManager").Get("MouseSens"));
			cam.Rotation = new Vector3(Mathf.Clamp(cam.Rotation.X - mouseEvent.Relative.Y * 0.005f * (float)GetNode("/root/OptionsManager").Get("MouseSens"),-Mathf.DegToRad(LockRot),Mathf.DegToRad(LockRot)) ,0,0);
		}
	}
}

using Godot;

namespace GodotTemplate.Scripts;
public partial class DynamicMovement : DynamicObject
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back
    }
    
    [Export] private Direction direction = Direction.Up;
    [Export] float movementAmount = 2.0f;
    [Export] float movementTime = 2.0f;

    private float elapsed = 0;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isMoving = false; // Track if movement is in progress
    
    Vector3 MovementVector
    {
        get
        {
            switch (direction)
            {
                case Direction.Up:    return Vector3.Up;
                case Direction.Down:  return Vector3.Down;
                case Direction.Left:  return Vector3.Left;
                case Direction.Right: return Vector3.Right;
                case Direction.Forward:return Vector3.Forward;
                case Direction.Back:  return Vector3.Back;
                default:              return Vector3.Zero;
            }
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (isMoving)
        {
            MoveObject(delta);
        }
    }

    public override void Activate()
    {
        if (elapsed > 0) return; // Avoid reactivating if already active

        base.Activate();
        
        startPosition = Position;
        endPosition = Position + MovementVector * (directionReversed ? -1 : 1) * movementAmount;
        elapsed = 0; // Reset elapsed time
        isMoving = true; // Enable movement
        
        
    }

    private void MoveObject(double delta)
    {
        if (elapsed < movementTime)
        {
            elapsed += (float)delta;
            float t = elapsed / movementTime;
            Position = startPosition.Lerp(endPosition, t);
        }
        else
        {
            Position = endPosition; // Ensure the final position is set precisely
            isMoving = false; // Stop movement
            elapsed = 0;
        }
    }
}
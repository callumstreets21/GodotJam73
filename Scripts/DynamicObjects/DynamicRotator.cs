using Godot;

namespace GodotTemplate.Scripts;

public partial class DynamicRotator : DynamicObject
{
    [Export] private float rotationAmount = 90.0f; // Total degrees to rotate
    [Export] private float rotationTime = 2.0f; // Time over which to complete the rotation

    private float elapsed = 0; // Elapsed time since the start of rotation
    private bool isRotating = false; // Track if rotation is in progress
    private Quaternion startRotation;
    private Quaternion endRotation;
    
    public override void _Process(double delta)
    {
        base._Process(delta);
        if (isRotating)
        {
            RotateObject(delta);
        }
    }

    public override void Activate()
    {
        if (onceOnly && hasActivated) return;
        if (isRotating) return; // Avoid reactivation while already rotating
        
        base.Activate(); // Must be first
        
        // Calculate end rotation based on current and desired rotation amount
        Vector3 axis = Vector3.Up; // Assuming rotation around Y axis
        
        // Initialize the starting rotation quaternion
        startRotation = new Quaternion(Transform.Basis);
        endRotation = startRotation * new Quaternion(axis, Mathf.DegToRad(rotationAmount * (directionReversed ? -1 : 1)));
        
        elapsed = 0; // Reset the timer
        isRotating = true; // Start rotation
    }

    private void RotateObject(double delta)
    {
        elapsed += (float)delta;
        if (elapsed < rotationTime)
        {
            float t = elapsed / rotationTime; // Calculate interpolation parameter
            Quaternion currentRotation = startRotation.Slerp(endRotation, t); // Spherical linear interpolation
            Rotation = currentRotation.GetEuler(); // Convert quaternion to Euler angles for setting rotation
        }
        else
        {
            Rotation = endRotation.GetEuler(); // Ensure exact final rotation is set
            isRotating = false; // Stop rotation
        }
    }
}

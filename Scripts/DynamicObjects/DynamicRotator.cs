using Godot;

namespace GodotTemplate.Scripts;

public partial class DynamicRotator : DynamicObject
{
    [Export] float rotationAmount = 90.0f;

    public override void Activate()
    {
        base.Activate();
        Rotate();
    }

    private void Rotate()
    {
        Rotate(Vector3.Forward, rotationAmount);
    }
}
using Godot;
using System;
using System.Collections.Generic;

namespace GodotTemplate.Scripts;

public partial class ActivatorPressurePad : Activator
{
    [Export] private bool triggerOnExit = false;
    [Export] private float depressionDistance = 0.05f;

    private MeshInstance3D mesh;
    private Queue<Action> actionsQueue = new Queue<Action>();
    private bool isProcessing = false;

    public override void _Ready()
    {
        base._Ready();
        mesh = Utils.FindComponentInChildren<MeshInstance3D>(this);
    }

    public void _on_area_3d_body_entered(Node other)
    {
        if (!other.IsInGroup("Player")) return;

        mesh.Position -= new Vector3(0, depressionDistance, 0);
        QueueUse();
    }

    public void _on_area_3d_body_exited(Node other)
    {
        if (!other.IsInGroup("Player")) return;

        mesh.Position += new Vector3(0, depressionDistance, 0);
        if (triggerOnExit) QueueUse();
    }

    private void QueueUse()
    {
        actionsQueue.Enqueue(Use);
        if (!isProcessing)
        {
            ProcessQueue();
        }
    }

    private void ProcessQueue()
    {
        if (actionsQueue.Count == 0)
        {
            isProcessing = false;
            return;
        }

        isProcessing = true;
        Action action = actionsQueue.Dequeue();
        action.Invoke();
        // Consider using a delay or checking conditions before processing the next item.
        CallDeferred(nameof(ProcessQueue));
    }
}
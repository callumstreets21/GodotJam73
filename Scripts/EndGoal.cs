using Godot;

[Tool]
public partial class EndGoal : Node3D
{
	private PackedScene _nextLevel;
	private string _nextLevelPath = "";

	[Export]
	public PackedScene NextLevel
	{
		get => _nextLevel;
		set
		{
			_nextLevel = value;
			_nextLevelPath = _nextLevel != null ? _nextLevel.ResourcePath : "";
		}
	}
	
	public override void _Ready()
	{
		var area = GetNode<Area3D>("Area3D"); // Relative path to the child node
		area.BodyEntered += _on_Area3D_body_entered;
	}
	
	public void _on_Area3D_body_entered(Node3D other)
	{
		if (!other.IsInGroup("Player")) return;
		
		var levelManager = GetNode("/root/LevelManager");
		if (levelManager != null)
		{
			if (!string.IsNullOrEmpty(_nextLevelPath)) levelManager.Call("load_scene_by_path", _nextLevelPath);
			else 									  levelManager.Call("reload_scene");
		}
		else
		{
			GD.PrintErr("LevelManager not found in the scene tree.");
		}
	}
}

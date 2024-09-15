using Godot;
using System;

public partial class SeetingsMenuMasterScr : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void GameSetRes(Vector2I NewS){
		GetWindow().Size = NewS;
		//ProjectSettings.SetSetting("display/window/size/width",NewS.X);
		//ProjectSettings.SetSetting("display/window/size/height",NewS.Y);
		//ProjectSettings.Save();
	}

	public void Toggle_Vsync(){
		DisplayServer.WindowSetVsyncMode((DisplayServer.WindowGetVsyncMode() == DisplayServer.VSyncMode.Enabled ? DisplayServer.VSyncMode.Disabled : DisplayServer.VSyncMode.Enabled));
	}
	public void Set_FOV(float value, NodePath Text_Path){
		GetNode<Label>(Text_Path).Text = ("FOV : " + value.ToString());
		//global
	}
}

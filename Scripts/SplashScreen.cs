using Godot;
using System;

public partial class SplashScreen : Node2D
{
	public void _on_timer_timeout()
	{
		var levelManager = GetNode("/root/LevelManager");
		if (levelManager != null)
		{
			levelManager.Call("show_main_menu");
		}
	}
}

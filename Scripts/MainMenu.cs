using System.Net.Mime;
using Godot;

public partial class MainMenu : Control
{
	public void _on_start_button_pressed()
	{
		var levelManager = GetNode("/root/LevelManager");
		levelManager?.Call("show_level_1");
	}

	public void _on_options_button_pressed()
	{
		var levelManager = GetNode("/root/LevelManager");
		levelManager.Call("show_options_menu");
	}

	public void _on_exit_button_pressed()
	{
		GetTree().Quit();
	}
}

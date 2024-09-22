using Godot;
using System;
using GodotTemplate.Scripts;

public partial class OptionsMenu : Control
{
	// Constants
	private const string OPTIONS_MANAGER_PATH = "/root/OptionsManager";
	public void _on_back_button_pressed()
	{
		var levelManager = GetNode("/root/LevelManager");
		levelManager.Call("show_main_menu");
	}

	public void _on_master_volume_slider_value_changed(float value)
	{
		// Accessing the GDScript 'OptionsManager' to retrieve volumes
		var optionsManager = GetNode(OPTIONS_MANAGER_PATH);  
		optionsManager.Call("SetMasterVolume", value);
		AudioManager.Instance.UpdateMusicVolume();
	}

	public void _on_music_volume_slider_value_changed(float value)
	{
		// Accessing the GDScript 'OptionsManager' to retrieve volumes
		var optionsManager = GetNode(OPTIONS_MANAGER_PATH);  
		optionsManager.Call("SetMusicV", value);
		AudioManager.Instance.UpdateMusicVolume();
	}

	public void _on_sfx_volume_slider_value_changed(float value)
	{
		// Accessing the GDScript 'OptionsManager' to retrieve volumes
		var optionsManager = GetNode(OPTIONS_MANAGER_PATH);  
		optionsManager.Call("SetSFXV", value);
	}

	public void _on_look_sensetivity_slider_value_changed(float value)
	{
		// Accessing the GDScript 'OptionsManager' to retrieve volumes
		var optionsManager = GetNode(OPTIONS_MANAGER_PATH);  
		optionsManager.Call("SetSensitivity", value);
	}
}

using Godot;
using System;
using GodotTemplate.Scripts;

public partial class OptionsMenuIngame : Control
{
	// Constants
	private const string OPTIONS_MANAGER_PATH = "/root/OptionsManager";

	public override void _Ready()
	{
		HSlider MVolume = GetNode<HSlider>("Options Backing/Master Volume/Master Volume Slider");
		MVolume.Value = GetNode(OPTIONS_MANAGER_PATH).Call("get_master_volume").AsDouble();
		HSlider MuVolume = GetNode<HSlider>("Options Backing/Music Volume/Music Volume Slider");
		MuVolume.Value = GetNode(OPTIONS_MANAGER_PATH).Call("get_music_volume").AsDouble();
		HSlider SFXVolume = GetNode<HSlider>("Options Backing/SFX Volume/SFX Volume Slider");
		SFXVolume.Value = GetNode(OPTIONS_MANAGER_PATH).Call("get_sfx_volume").AsDouble();
		HSlider Sense = GetNode<HSlider>("Options Backing/Look Sensetivity/Look Sensetivity Slider");
		Sense.Value = GetNode(OPTIONS_MANAGER_PATH).Call("GetLookSensetivity").AsDouble();
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("pause"))
		{
			Visible = !Visible;
			Input.MouseMode = Visible ? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
			GetTree().Paused = Visible ? true : false;
		}
	}
	
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

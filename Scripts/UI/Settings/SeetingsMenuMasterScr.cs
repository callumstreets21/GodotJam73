using Godot;
using System;

public partial class SeetingsMenuMasterScr : Node2D
{
	//to simply references for control buttons
	[Export] public Button[] ControlButtons;
	[Export] private string[] ControlInputs = {"move_up","move_down","move_left","move_right","jump"};
	public bool Active_Button_Input = false;
	public int SelectControlButton = -1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventKey keyEvent && keyEvent.Pressed){
			if(SelectControlButton >= 0 && keyEvent.Keycode != Key.Escape){
				InputMap.ActionEraseEvents(ControlInputs[SelectControlButton]);
				InputMap.ActionAddEvent(ControlInputs[SelectControlButton],@event);
				GD.Print(@event.AsText());
			}
			SelectControlButton = -1;
			Deactivate_Buttons();
		}
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
	public void Get_Vsync(NodePath Path){
		GetNode<Button>(Path).ButtonPressed = (DisplayServer.WindowGetVsyncMode() == DisplayServer.VSyncMode.Enabled);
	}
	public void Set_FOV(float value, NodePath Text_Path){
		GetNode<Label>(Text_Path).Text = ("FOV : " + value.ToString());
		GD.Print(GetNode("/root/OptionsManager") != null);
		GetNode("/root/OptionsManager").Call("SetFOV",value);
		//global
	}

	public void Get_Mono(NodePath Path){
		GetNode<Button>(Path).ButtonPressed = (bool)GetNode("/root/OptionsManager").Get("MonoAudio");
	}
	public void Set_Mono(){
		GetNode("/root/OptionsManager").Set("MonoAudio",!(bool)GetNode("/root/OptionsManager").Get("MonoAudio"));
	}

	private void Deactivate_Buttons(int ex = -1){
		for(int l = 0; l < ControlButtons.Length; l++)
			if (l != ex)
				ControlButtons[l].ButtonPressed = false;
	}
	public void Set_Button(int b){
		Deactivate_Buttons(b);
		SelectControlButton = b;
	}

	public void Set_Sensitivity(float value, NodePath Text_Path){
		GetNode<Label>(Text_Path).Text = ("Sensitivity : " + value.ToString());
		GetNode("/root/OptionsManager").Call("SetSensitivity",value);
	}

	public void Get_Music_V(NodePath S_Path, NodePath L_Path){
		float get_M = GetTControlerVolume(0);
		GetNode<Label>(L_Path).Text = ("Music : " + ((get_M/48)+0.5 > -24 ? ((get_M/48)+0.5).ToString().PadDecimals(3) : "X"));
		GetNode<Slider>(S_Path).Value = get_M;
	}
	public void Set_Music_Volume(float value, NodePath Path){
		GetNode("/root/OptionsManager").Call("SetMusicV",value);
		GetNode<Label>(Path).Text = ("Music : " + (value > -24 ? ((value/48)+0.5).ToString().PadDecimals(3) : "X"));
	}
	public void Get_SFX_V(NodePath S_Path, NodePath L_Path){
		float get_S = GetTControlerVolume(1);
		GetNode<Label>(L_Path).Text = ("SFX : " + (get_S > -24 ? ((get_S/48)+0.5).ToString().PadDecimals(3) : "X"));
		GetNode<Slider>(S_Path).Value = get_S;
		//GetNode<Button>(Path).ButtonPressed = (DisplayServer.WindowGetVsyncMode() == DisplayServer.VSyncMode.Enabled);
	}
	public void Set_SFX_Volume(float value, NodePath Path){
		GetNode("/root/OptionsManager").Call("SetSFXV",value);
		GetNode<Label>(Path).Text = ("SFX : " + (value > -24 ? ((value/48)+0.5).ToString().PadDecimals(3) : "X"));
	}

	private float GetTControlerVolume(int Slider = -1){
		float get_V = 0;
		if(Slider==0){
			get_V = (float)GetNode("/root/OptionsManager").Get("MusicVolume");
		}else if(Slider == 1){
			get_V = (float)GetNode("/root/OptionsManager").Get("SFXVolume");
		}
		return get_V;
	}
}

using Godot;
using System;

public partial class SettingTabBarSelect : Control
{
	[Export] public OptionButton[] TabButtons;
	[Export] public float PaddingH = 2.0f;
	[Export] public float PaddingV = 2.0f;

	[Export] public Control[] TabMenus;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ICloseAllMenu();
		for(int i=0; i < TabButtons.Length; i++){
			TabButtons[i].Size = new Vector2((Size.X / TabButtons.Length)  - (PaddingH), TabButtons[i].Size.Y - (PaddingV));
			TabButtons[i].Position = new Vector2((Size.X / TabButtons.Length)*i + (PaddingH/2), + (PaddingV/2));
			if (i < TabMenus.Length)
				TabButtons[i].Pressed += () => OpenMenu(i);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ICloseAllMenu(){
		foreach (Control Menu in TabMenus){
			Menu.Visible = false;
			Menu.ProcessMode = ProcessModeEnum.Disabled;
		}
	}
	public void OpenMenu(int i){
		GD.Print(TabMenus[i].Name);
		ICloseAllMenu();
		Control Menu = TabMenus[i];
		Menu.Visible = true;
		Menu.ProcessMode = ProcessModeEnum.Inherit;
	}
}

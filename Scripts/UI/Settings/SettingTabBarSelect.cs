using Godot;
using System;

public partial class SettingTabBarSelect : Control
{
	[Export] public MenuButton[] TabButtons;
	[Export] public float PaddingH = 2.0f;
	[Export] public float PaddingV = 2.0f;

	[Export] public Control[] TabMenus;
	int ButtonMenuLength = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ICloseAllMenu();
		for(int i=0; i < TabButtons.Length; i++){
			//Set tab size
			TabButtons[i].Size = new Vector2((Size.X / TabButtons.Length)  - (PaddingH), TabButtons[i].Size.Y - (PaddingV));
			TabButtons[i].Position = new Vector2((Size.X / TabButtons.Length)*i + (PaddingH/2), + (PaddingV/2));
			//update button id
			for(int t=0; t < TabButtons[i].GetPopup().ItemCount; t++)
				TabButtons[i].GetPopup().SetItemId(t,TabButtons[i].GetPopup().GetItemId(t)+ButtonMenuLength);
			//connect button methods
			TabButtons[i].GetPopup().Connect("id_pressed", new Callable(this, MethodName.OpenMenu));
			ButtonMenuLength += TabButtons[i].ItemCount;
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
	private void OpenMenu(long id){
		GD.Print(id);
		ICloseAllMenu();
		Control Menu = TabMenus[id];
		Menu.Visible = true;
		Menu.ProcessMode = ProcessModeEnum.Inherit;
	}
}

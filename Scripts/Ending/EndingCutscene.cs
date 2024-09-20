using Godot;
using System;

public partial class EndingCutscene : Node3D
{
	[Export] public Node3D Scene;
	[Export] public VideoStreamPlayer VidPlayer;
	[Export] public Node3D PlBody;
	[Export] public Control UIScene;
	[Export] public Button Bext;
	private string DeafultBText;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Visible;

		Scene.Visible = false;
		Scene.ProcessMode = ProcessModeEnum.Disabled;
		VidPlayer.Finished += EndAnim;
		UIScene.Visible = false;

		DeafultBText = Bext.Text;
		Bext.MouseEntered += () => SetButtonText(Bext, "Back To Menu?");
		Bext.MouseExited += () => SetButtonText(Bext, DeafultBText);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void EndAnim(){
		Scene.Visible = true;
		Scene.ProcessMode = ProcessModeEnum.Inherit;
		VidPlayer.QueueFree();
		PlBody.GetNode<AnimationPlayer>("AnimationPlayer").Play("Idle1");
		UIScene.Visible = true;
	}

	public void SetButtonText(Button B, String text){
		B.Text = text;
	}
}

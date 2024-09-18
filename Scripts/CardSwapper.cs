using Godot;
using System;

public partial class CardSwapper : Node2D
{
	[Export] private Sprite2D duckCard;
	public void _on_timer_timeout()
	{
		
		duckCard.ZIndex = duckCard.ZIndex < 1 ? 2 : 0;
	}
}

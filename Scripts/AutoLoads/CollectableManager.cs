using Godot;
using System;

public partial class CollectableManager : Node
{	
	// This is a singleton class, so we can access it from anywhere.
	public static CollectableManager Instance { get; private set; }
	
	private int _feathersCollected = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}
	
	public void CollectFeather()
	{
		_feathersCollected++;
		GD.Print("Feathers Collected: " + _feathersCollected);
	}
	
	public int GetFeathersCollected()
	{
		return _feathersCollected;
	}
	
}

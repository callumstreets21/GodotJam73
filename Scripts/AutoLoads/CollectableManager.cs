using Godot;
using System;

public partial class CollectableManager : Node
{	
	// This is a singleton class, so we can access it from anywhere.
	public static CollectableManager Instance { get; private set; }
	
	private int _feathersCollected = 0;
	private bool _hasFeather = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}
	
	public void CollectFeather()
	{
		_hasFeather = true;
		//GD.Print("Feathers Collected: " + _feathersCollected);
	}
	
	public void RemoveFeather()
	{
		_hasFeather = false;
	}
	
	public void ConfirmFeather()
	{
		if (_hasFeather)
		{
			_feathersCollected++;
			_hasFeather = false;
		}
		
	}
	
	public int GetFeathersCollected()
	{
		return _feathersCollected;
	}
	
	
	public void ResetFeatherCount()
	{
		_feathersCollected = 0;
	}
	
}

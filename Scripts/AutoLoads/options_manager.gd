extends Node
class_name Option_Manager

var FOV:float = 0
var MouseSens:float = 1.0

var MusicVolume:float = 0.0
var SFXVolume:float = 0.0

var MonoAudio:bool = false

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#Load FOV / MouseSens settnig Here
	SetFOV(FOV)
	SetSensitivity(MouseSens)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func SetFOV(value:float) -> void:
	if get_tree().current_scene != null && get_tree().get_node_count_in_group("Player") > 0:
		get_tree().get_nodes_in_group("Player")[0].get_node("Camera3D").FOV = value
	FOV = value

func SetSensitivity(value:float) -> void:
	#Mouse sensitivity add player setting
	MouseSens = value

#dosent need to be function but plays better with c#
func SetMusicV(value:float)->void:
	print("GHidhgahdaj")
	MusicVolume = value

#dosent need to be function but plays better with c#
func SetSFXV(value:float)->void:
	print("GHidhgahdaj")
	SFXVolume = value

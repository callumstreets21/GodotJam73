extends Node
class_name Option_Manager

var FOV:float = 0

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#Load FOV settnig Here
	SetFOV(FOV)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func SetFOV(value:float) -> void:
	if get_tree().current_scene != null && get_tree().get_node_count_in_group("Player") > 0:
		get_tree().get_nodes_in_group("Player")[0].get_node("Camera3D").FOV = value
		

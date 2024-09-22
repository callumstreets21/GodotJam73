extends Button

@onready var parent = $".."


func _on_pressed() -> void:
	get_tree().paused = false
	LevelManager.reload_scene()

func _on_back_button_3_pressed() -> void:
	parent.visible = not parent.visible
	Input.mouse_mode =Input.MouseMode.MOUSE_MODE_VISIBLE  if parent.visible else Input.MouseMode.MOUSE_MODE_CAPTURED
	get_tree().paused = false

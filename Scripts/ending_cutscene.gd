extends VideoStreamPlayer

func _ready() -> void:
	LevelManager.stop_timer()

func _on_finished() -> void:
	LevelManager.show_win_screen()

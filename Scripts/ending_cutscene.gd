extends VideoStreamPlayer

func _on_finished() -> void:
	LevelManager.show_win_screen()

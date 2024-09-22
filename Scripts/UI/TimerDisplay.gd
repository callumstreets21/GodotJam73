extends Label


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	LevelManager.stop_timer()
	text = str(LevelManager.timer) + " seconds"

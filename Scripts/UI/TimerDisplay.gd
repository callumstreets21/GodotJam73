extends Label

@onready var label_5: Label = $"../Label5"


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	text = str(LevelManager.timer) + " seconds"
	Input.mouse_mode = Input.MouseMode.MOUSE_MODE_VISIBLE
	label_5.text = str(CollectableManager.GetFeathersCollected()) + " / 7"

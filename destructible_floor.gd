extends Node3D
@onready var area_reference = $FloorTiles/Area3D
@onready var timer = $FloorTiles/Area3D/Timer
# Called when the node enters the scene tree for the first time.

func _on_area_3d_body_entered(body: Node3D) -> void:
	if (body is CharacterBody3D):
		timer.start(1)

func _on_timer_timeout() -> void:
	queue_free()

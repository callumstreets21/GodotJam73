extends Node

# Constants
const CREDITS_MENU = preload("res://Scenes/_menus/credits_menu.tscn")
const GAME_OVER_SCREEN = preload("res://Scenes/_menus/game_over_screen.tscn")
const MAIN_MENU = preload("res://Scenes/_menus/main_menu.tscn")
const OPTIONS_MENU = preload("res://Scenes/_menus/options_menu.tscn")
const SPLASH_SCREEN = preload("res://Scenes/_menus/splash_screen.tscn")
const WIN_SCREEN = preload("res://Scenes/_menus/win_screen.tscn")

# Runtime Variables
var current_scene = null

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	if current_scene == null:
		current_scene = SPLASH_SCREEN
	
func load_current_scene():
	_change_scene(current_scene)
	
func show_credits():
	_change_scene(CREDITS_MENU)

func show_game_over():
	_change_scene(GAME_OVER_SCREEN)
	
func show_main_menu():
	_change_scene(MAIN_MENU)
	
func show_options_menu():
	_change_scene(OPTIONS_MENU)
	
func show_win_screen():
	_change_scene(WIN_SCREEN)
	
func load_scene_by_path(scene: String):
	_change_scene(scene)
	
func reload_scene():
	_change_scene(current_scene)
	
func _change_scene(scene_path):
	# Load the new scene
	var scene_resource = load(scene_path)
	var new_scene = scene_resource.instantiate()
	
	# Remove the existing scene if it exists
	if current_scene:
		current_scene.queue_free()
		
	# Add the new scene to the root
	get_tree().root.add_child(new_scene)
	
	# Update the reference to the current scene
	current_scene = new_scene

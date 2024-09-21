extends SubViewport

@export var offset : Vector3;
# Called when the node enters the scene tree for the first time.
func _ready():
	debug_draw = 2


#adjust the position of the moveming section to follow the the parented node
func _process(delta):
	$MovingSection.global_position = get_parent().global_position+offset

#Used to poll the light level when needed
func get_light_level() -> float:
	var texture = get_texture()
	var colour = _get_average_colour(texture)
	return colour.get_luminance()
	
func _get_average_colour(texture: ViewportTexture) -> Color:
	var img = texture.get_image()
	img.resize(1,1, Image.INTERPOLATE_LANCZOS)
	return img.get_pixel(0,0)

using Godot;
using System;

namespace GodotTemplate.Scripts;

public partial class AudioManager : Node
{
    // Constants
    private const string OPTIONS_MANAGER_PATH = "/root/OptionsManager";
    
    // Statics
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;
    
    // Configurations
    [Export] private AudioStream ambientMusic;

    // Cached references
    private AudioStreamPlayer musicSource;
    private Camera3D mainCamera;
    
    public override void _EnterTree()
    {
        _instance = this;
    }

    public override void _ExitTree()
    {
        _instance = null;
    }
    
    public override void _Ready()
    {
        // Find the main camera in the scene and attach the AudioStreamPlayer to it
        mainCamera = GetViewport().GetCamera3D();
        if (mainCamera == null)
        {
            GD.Print("Main Camera not found, attaching music source to AudioManager node itself.");
            musicSource = new AudioStreamPlayer();
            AddChild(musicSource);
        }
        else
        {
            GD.Print("Main Camera found, attaching music source to it.");
            musicSource = new AudioStreamPlayer();
            mainCamera.AddChild(musicSource);
        }

        PlayTrack(ambientMusic);
    }

    public void PlayTrack(AudioStream track)
    {
        if (musicSource.Stream == track)
        {
            if (!musicSource.Playing) musicSource.Play();
        }
        else
        {
            musicSource.Stream = track;
            musicSource.Play();
        }

        // Accessing the GDScript 'OptionsManager' to retrieve volumes
        var optionsManager = GetNode(OPTIONS_MANAGER_PATH);  
        float masterVolume = (float)optionsManager.Call("get_master_volume");
        float musicVolume = (float)optionsManager.Call("get_music_volume");
        musicSource.VolumeDb = Utils.LinearToDb(masterVolume * musicVolume); 
        
    }

    public void PauseMusic()
    {
        musicSource.Stop();
    }

    /// <summary>
    /// Play an audio clip at a specified position in the world
    /// </summary>
    /// <param name="clip">The audio stream to play</param>
    /// <param name="playPosition">The world position to play the clip at, or null to use the current node position</param>
    /// <param name="volume">The volume at which to play the clip, before applying global volume settings</param>
    public void PlayClip(AudioStream clip, Vector3? playPosition = null, float volume = 1.0f)
    {
        if (clip == null)
        {
            GD.Print("Attempting to play clip when clip is null");
            return;
        }

        // If no position is specified, use the position of this node
        Vector3 position = playPosition ?? mainCamera.Position;
        
        // Create a temporary AudioStreamPlayer3D for playing the clip
        
        var audioPlayer = new AudioStreamPlayer3D();
        AddChild(audioPlayer);
        audioPlayer.Stream = clip;
        audioPlayer.Position = position;

        // Accessing the GDScript 'OptionsManager' to retrieve volumes
        var optionsManager = GetNode(OPTIONS_MANAGER_PATH);  
        float masterVolume = (float)optionsManager.Call("get_master_volume");
        float sfxVolume = (float)optionsManager.Call("get_sfx_volume");
        
        // Calculate final volume based on master and effects volumes
        float finalVolume = volume * masterVolume * sfxVolume;
        audioPlayer.SetVolumeDb(Utils.LinearToDb(finalVolume));

        audioPlayer.Play();
        
        // Automatically free the player after playback completes
        audioPlayer.Connect("finished", new Callable(audioPlayer, "queue_free"));

    }
}

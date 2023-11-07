using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResourceManager : MonoBehaviour
{
    public static AudioClip GetAudioClip(SoundEffect soundEffect)
    {
        if (!_soundEffectToPath.ContainsKey(soundEffect))
            throw new System.Exception("Sound effect doesn't have audio clip");

        return Resources.Load<AudioClip>(_soundEffectToPath[soundEffect]);
    }

    public static AudioClip GetAudioClip(Music music)
    {
        if (!_musicToPath.ContainsKey(music))
            throw new System.Exception("Music doesn't have audio clip");

        return Resources.Load<AudioClip>(_musicToPath[music]);
    }

    //Sound effects
    public enum SoundEffect
    {
        MenuClick,
        SliderClick,
        LevelStart,
        LevelFinished,
        LevelFailed,
        PlayerHit,
        BonusPickup,
        CheckpointActivated,
        TeleportStarted,
        TeleportFinished,
        NewPathPoint
    }

    const string _audioFilesDirectoryPath = "Audio/SoundEffects/";

    private static Dictionary<SoundEffect, string> _soundEffectToPath = new Dictionary<SoundEffect, string>
    {
        { SoundEffect.PlayerHit, _audioFilesDirectoryPath + "PlayerHit" },
        { SoundEffect.BonusPickup, _audioFilesDirectoryPath + "BonusPickup" },
        { SoundEffect.CheckpointActivated, _audioFilesDirectoryPath + "CheckpointActivated" },
        { SoundEffect.TeleportStarted, _audioFilesDirectoryPath + "TeleportStarted" },
        { SoundEffect.TeleportFinished, _audioFilesDirectoryPath + "TeleportFinished" },
        { SoundEffect.NewPathPoint, _audioFilesDirectoryPath + "NewPathPoint" },
    };

    //Music
    public enum Music
    {
        MenuMusic,
        LevelMusic
    }

    const string _musicFilesDirectoryPath = "Audio/Music/";

    private static Dictionary<Music, string> _musicToPath = new Dictionary<Music, string>
    {
        { Music.MenuMusic, _musicFilesDirectoryPath + "MenuMusic" },
        { Music.LevelMusic, _musicFilesDirectoryPath + "LevelMusic" }
    };

    public enum Level
    {
        Level1
    }

    [SerializeField]
    private static string _pathToLevels = "assets/Prefabs/Levels/";

    [SerializeField]
    private static Dictionary<Level, string> _levels = new Dictionary<Level,string>()
    {
        {Level.Level1, _pathToLevels + "Level1.prefab"}
    };

    public static GameObject LoadLevel(Level levelToLoad)
    {
        if (_levels.ContainsKey(levelToLoad))
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(_levels[levelToLoad]);
        }
        else
        {
            Debug.LogError($"Level {levelToLoad} not found");
            return null;
        }
    }    

}

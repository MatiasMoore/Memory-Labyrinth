using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    const string audioFilesDirectoryPath = "Audio/SoundEffects/";

    private static Dictionary<SoundEffect, string> _soundEffectToPath = new Dictionary<SoundEffect, string>
    {
        { SoundEffect.PlayerHit, audioFilesDirectoryPath + "PlayerHit" },
        { SoundEffect.BonusPickup, audioFilesDirectoryPath + "BonusPickup" },
        { SoundEffect.CheckpointActivated, audioFilesDirectoryPath + "CheckpointActivated" },
        { SoundEffect.TeleportStarted, audioFilesDirectoryPath + "TeleportStarted" },
        { SoundEffect.TeleportFinished, audioFilesDirectoryPath + "TeleportFinished" },
        { SoundEffect.NewPathPoint, audioFilesDirectoryPath + "NewPathPoint" },
    };

    //Music
    public enum Music
    {
        MenuMusic,
        LevelMusic
    }

    const string musicFilesDirectoryPath = "Audio/Music/";

    private static Dictionary<Music, string> _musicToPath = new Dictionary<Music, string>
    {
        { Music.MenuMusic, musicFilesDirectoryPath + "MenuMusic" },
        { Music.LevelMusic, musicFilesDirectoryPath + "LevelMusic" }
    };

}

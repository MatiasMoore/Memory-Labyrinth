using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourceManager;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if (Instance != null) return;

        Instance = this;
    }

    public void PlayMusic(ResourceManager.Music music)
    {
        var musicClip = ResourceManager.GetAudioClip(music);
        if (musicClip == null)
            throw new System.Exception("Audio file for music " + music.ToString() + " couldn't be found");
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        _audioSource.clip = musicClip;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void PauseMusic()
    {
        _audioSource.Pause();
    }

    public void ResumeMusic()
    {
        _audioSource.UnPause();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

}

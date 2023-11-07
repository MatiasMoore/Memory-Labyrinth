using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
        _audioSource.clip = musicClip;
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

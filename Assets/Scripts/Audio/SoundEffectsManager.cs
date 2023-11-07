using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectsManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public static SoundEffectsManager Instance { get; private set; }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if (Instance != null) return;

        Instance = this;
    }

    public void PlaySoundEffect(ResourceManager.SoundEffect soundEffect)
    {
        Debug.Log("Audio: Playing sound effect");
        var clip = ResourceManager.GetAudioClip(soundEffect);
        _audioSource.PlayOneShot(clip);
    }

    public void PlaySoundEffect(ResourceManager.SoundEffect soundEffect, AudioSource audioSource)
    {
        var clip = ResourceManager.GetAudioClip(soundEffect);
        audioSource.PlayOneShot(clip);
    }

}

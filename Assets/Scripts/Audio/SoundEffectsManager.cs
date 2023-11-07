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
        var clip = ResourceManager.GetAudioClip(soundEffect);
        if (clip == null)
            throw new System.Exception("Audio file for sound effect " + soundEffect.ToString() + " couldn't be found");
        _audioSource.PlayOneShot(clip);
    }

    public void PlaySoundEffect(ResourceManager.SoundEffect soundEffect, AudioSource audioSource)
    {
        var clip = ResourceManager.GetAudioClip(soundEffect);
        if (clip == null)
            throw new System.Exception("Audio file for sound effect " + soundEffect.ToString() + " couldn't be found");
        audioSource.PlayOneShot(clip);
    }

}

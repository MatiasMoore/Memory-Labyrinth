using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerControl : MonoBehaviour
{
    public AudioMixer _audioMixer;

    public void SetSfxVolume(float volCoef)
    {
        _audioMixer.SetFloat("SfxVolume", NormalisedToValue(volCoef));
    }

    public void SetMusicVolume(float volCoef)
    {
        _audioMixer.SetFloat("MusicVolume", NormalisedToValue(volCoef));
    }

    private float NormalisedToValue(float t)
    {
        t = Mathf.Lerp(-20f, 20f, t);
        if (t == -20)
            t = -80;
        return t;
    }
}
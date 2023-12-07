using MemoryLabyrinth.Resources;
using System.Collections;
using UnityEngine;

namespace MemoryLabyrinth.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        private AudioSource _currentTrack;
        private AudioSource _previousTrack;

        public static MusicManager Instance { get; private set; }

        private void Awake()
        {
            _currentTrack = GetComponent<AudioSource>();
            _currentTrack.loop = true;

            if (Instance != null) return;

            Instance = this;
        }

        public void PlayMusicInstant(ResourceManager.Music music)
        {
            var musicClip = ResourceManager.GetAudioClip(music);
            if (musicClip == null)
                throw new System.Exception("Audio file for music " + music.ToString() + " couldn't be found");

            _currentTrack.clip = musicClip;
            _currentTrack.Play();
        }

        public void PlayMusicWithBlending(ResourceManager.Music music, float timeToFinishBlend = 2.25f)
        {
            var musicClip = ResourceManager.GetAudioClip(music);
            if (musicClip == null)
                throw new System.Exception("Audio file for music " + music.ToString() + " couldn't be found");

            StopAllCoroutines();
            Destroy(_previousTrack);
            StartCoroutine(BlendVolume(musicClip, timeToFinishBlend));
        }

        private IEnumerator BlendVolume(AudioClip clip, float timeToFinishBlend)
        {
            bool hasPrevClip = _currentTrack.clip != null;

            //Create an audio source for previous track
            if (hasPrevClip)
            {
                _previousTrack = gameObject.AddComponent<AudioSource>();

                //Copy the current track and play
                _previousTrack.loop = _currentTrack.loop;
                _previousTrack.clip = _currentTrack.clip;
                _previousTrack.time = _currentTrack.time;
                _previousTrack.outputAudioMixerGroup = _currentTrack.outputAudioMixerGroup;
                _previousTrack.Play();
            }

            //Switch the current track to the new clip
            _currentTrack.Stop();
            _currentTrack.clip = clip;
            _currentTrack.Play();

            float currentVolume = _currentTrack.volume;

            //Lerp the previous track's volume to 0 and the current track's volume to 1
            float t = 0;
            while (t < 1)
            {
                if (hasPrevClip)
                    _previousTrack.volume = Mathf.Lerp(currentVolume, 0, t);
                _currentTrack.volume = Mathf.Lerp(0, 1, t);
                t += Time.unscaledDeltaTime / timeToFinishBlend;
                yield return null;
            }

            //Set the current track's volume to be an exact maximum
            _currentTrack.volume = 1;

            //Remove previous track's audio source
            if (hasPrevClip)
                Destroy(_previousTrack);
        }

        public void PauseMusic()
        {
            _currentTrack.Pause();
        }

        public void ResumeMusic()
        {
            _currentTrack.UnPause();
        }

        public void StopMusic()
        {
            _currentTrack.Stop();
        }
    }
}
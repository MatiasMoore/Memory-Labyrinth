using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad
{
    public sealed class SettingsStorage : MonoBehaviour
    {
        public static SettingsStorage Instance;

        [SerializeField]
        private SettingsData _settingsData;

        public void Init()
        {
            if (Instance != null)
                return;
          
            Instance = this;
        }

        public void SetupSfxVolume(float sfxVol)
        {
            this._settingsData._sfxVol = sfxVol;
        }

        public void SetupMusicVolume(float musicVol)
        {
            this._settingsData._musicVol = musicVol;
        }

        public void SetupSettings(SettingsData settingsData) 
        {
            SetupMusicVolume(settingsData._musicVol);
            SetupSfxVolume(settingsData._sfxVol);
        }

        public float GetSfxVolume()
        {
            return this._settingsData._sfxVol;
        }

        public float GetMusicVolume()
        {
            return this._settingsData._musicVol;
        }
    }
}
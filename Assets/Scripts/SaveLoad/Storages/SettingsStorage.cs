using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad
{
    public sealed class SettingsStorage : MonoBehaviour
    {
        public static SettingsStorage Instance;

        [SerializeField]
        private Settings _settings = new Settings();



        public const float DEFAULT_VALUE = 0.5f;

        public void Init()
        {
            if (Instance != null)
                return;
            _settings._settingsData = new List<SettingsData>();
            Instance = this;
        }

        public void SetupSetting(SettingsData setting) 
        {

            int settingIndex = _settings._settingsData.FindIndex(item => item._audioSetting == setting._audioSetting);

            if (settingIndex != -1)
            {
                _settings._settingsData[settingIndex] = setting;
            }
            else
            {
                _settings._settingsData.Add(setting);
            }
        }

        public void SetupSettings(List<SettingsData> settingsDatas) 
        {
            if (settingsDatas != null)
            {
                foreach (SettingsData data in settingsDatas)
                {
                    SettingsStorage.Instance.SetupSetting(data);
                }
            }
            else
            {
                _settings._settingsData = new List<SettingsData>();
                _settings._settingsData.Add(new SettingsData { _audioSetting = AudioSetting.SFX, _volume = DEFAULT_VALUE });
                _settings._settingsData.Add(new SettingsData { _audioSetting = AudioSetting.Music, _volume = DEFAULT_VALUE });
            }
        }

        public List<SettingsData> GetSettingsList()
        {
            return _settings._settingsData;
        }

        public SettingsData GetSettingsData(AudioSetting audioSetting) 
        {
            int settingIndex = _settings._settingsData.FindIndex(item => item._audioSetting == audioSetting);
            if (settingIndex != -1) return _settings._settingsData[settingIndex];
            else return new SettingsData { _audioSetting = audioSetting, _volume = DEFAULT_VALUE };
        }
    }
}
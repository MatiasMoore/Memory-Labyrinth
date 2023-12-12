using System;
using System.Collections.Generic;

namespace MemoryLabyrinth.SaveLoad
{
    [Serializable]
    public struct SettingsData
    {
        public AudioSetting _audioSetting;
        public float _volume;
    }

    [Serializable]
    public struct Settings
    {
        public List<SettingsData> _settingsData;
    }

    public enum AudioSetting 
    { 
        SFX,
        Music
    }

    public class SettingsSaveLoader : ISaveLoader
    {
        public void LoadData()
        {
            Settings data = Repository.GetData<Settings>();
            SettingsStorage.Instance.SetupSettings(data._settingsData);
        }

        public void SaveData()
        {
            if (SettingsStorage.Instance == null)
                return;
            List<SettingsData> data = SettingsStorage.Instance.GetSettingsList();
            Settings settings = new Settings();
            settings._settingsData = data;
            Repository.SetData(settings);
        }

    }
}
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

    public enum AudioSetting 
    { 
        SFX,
        Music
    }

    public class SettingsSaveLoader : ISaveLoader
    {
        public void LoadData()
        {
            List<SettingsData> data = Repository.GetData<List<SettingsData>>();
            SettingsStorage.Instance.SetupSettings(data);
        }

        public void SaveData()
        {
            if (SettingsStorage.Instance == null)
                return;
            List<SettingsData> data = SettingsStorage.Instance.GetSettingsList();

            Repository.SetData(data);
        }

    }
}
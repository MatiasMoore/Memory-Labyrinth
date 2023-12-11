using System;

namespace MemoryLabyrinth.SaveLoad
{
    [Serializable]
    public struct SettingsData
    {
        public float _sfxVol;
        public float _musicVol;
    }

    public class SettingsSaveLoader : ISaveLoader
    {
        public void LoadData()
        {
            SettingsData data = Repository.GetData<SettingsData>();
            SettingsStorage.Instance.SetupSettings(data);
        }

        public void SaveData()
        {
            if (SettingsStorage.Instance == null)
                return;
            float sfxVol = SettingsStorage.Instance.GetSfxVolume();
            float musicVol = SettingsStorage.Instance.GetMusicVolume();
            var data = new SettingsData
            {
                _sfxVol = sfxVol,
                _musicVol = musicVol
            };
            Repository.SetData(data);
        }

    }
}
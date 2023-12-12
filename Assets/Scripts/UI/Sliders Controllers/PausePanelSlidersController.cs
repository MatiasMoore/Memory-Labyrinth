using MemoryLabyrinth.SaveLoad;
using UnityEngine;

namespace MemoryLabyrinth.UI.SlidersLib
{
    public class PausePanelSlidersController : SlidersController
    {
        [SerializeField]
        private SFXSlider _sfxSlider;
        [SerializeField]
        private MusicSlider _musicSlider;

        private void OnDisable()
        {
            SaveAudioSettings();
        }

        public override void SaveAudioSettings()
        {
            SettingsData sfxSetting = new SettingsData { _audioSetting = AudioSetting.SFX, _volume = _sfxSlider.GetSliderValue() };
            SettingsStorage.Instance.SetupSetting(sfxSetting);

            SettingsData musicSetting = new SettingsData { _audioSetting = AudioSetting.Music, _volume = _musicSlider.GetSliderValue() };
            SettingsStorage.Instance.SetupSetting(musicSetting);

            SaveLoadManager.Instance.SaveGame();
        }
    }
}

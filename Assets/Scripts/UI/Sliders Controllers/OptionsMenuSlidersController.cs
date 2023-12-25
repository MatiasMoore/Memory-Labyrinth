using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.UI.ButtonsLib;
using UnityEngine;

namespace MemoryLabyrinth.UI.SlidersLib
{
    public class OptionsMenuSlidersController : SlidersController
    {
        [SerializeField]
        private OptionsMenuAcceptButton _acceptButton;
        
        [SerializeField]
        private SFXSlider _sfxSlider;
        [SerializeField]
        private MusicSlider _musicSlider;

        private void Start()
        {
            _acceptButton._buttonClick += SaveAudioSettings;
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

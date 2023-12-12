using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.UI.Button;
using MemoryLabyrinth.UI.SliderLib;
using UnityEngine;

namespace MemoryLabyrinth.UI.OptionsLib
{
    public class OptionsMenuController : MonoBehaviour
    {
        [SerializeField]
        private OptionsMenuAcceptButton _acceptButton;
        [SerializeField]
        private OptionsMenuCancelButton _cancelButton;
        
        [SerializeField]
        private SFXSlider _sfxSlider;
        [SerializeField]
        private MusicSlider _musicSlider;

        private void Start()
        {
            _acceptButton._buttonClick += SaveAudioSettings;
        }

        private void SaveAudioSettings()
        {
            SettingsData sfxSetting = new SettingsData { _audioSetting = AudioSetting.SFX, _volume = _sfxSlider.GetSliderValue() };
            SettingsStorage.Instance.SetupSetting(sfxSetting);

            SettingsData musicSetting = new SettingsData { _audioSetting = AudioSetting.Music, _volume = _musicSlider.GetSliderValue() };
            SettingsStorage.Instance.SetupSetting(musicSetting);
        }
    }
}

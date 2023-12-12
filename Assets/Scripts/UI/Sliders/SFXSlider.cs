using MemoryLabyrinth.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryLabyrinth.UI.SlidersLib
{
    public class SFXSlider: AbstractSlider
    {
        [SerializeField]
        private Slider slider;

        private void OnEnable()
        {
            float volume = SettingsStorage.Instance.GetSettingsData(AudioSetting.SFX)._volume;
            SetSliderValue(volume);
        }

        public override void SetSliderValue(float volume)
        {
            slider.value = volume;
        }

        public override float GetSliderValue()
        {
            return slider.value;
        }
    }
}
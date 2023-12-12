using MemoryLabyrinth.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryLabyrinth.UI.SliderLib
{
    public class SFXSlider: MonoBehaviour
    {
        [SerializeField]
        private Slider slider;

        private void OnEnable()
        {
            float volume = SettingsStorage.Instance.GetSettingsData(AudioSetting.SFX)._volume;
            SetSliderValue(volume);
        }

        public void SetSliderValue(float volume)
        {
            slider.value = volume;
        }

        public float GetSliderValue()
        {
            return slider.value;
        }
    }
}
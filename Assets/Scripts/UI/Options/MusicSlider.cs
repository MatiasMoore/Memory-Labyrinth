using MemoryLabyrinth.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryLabyrinth.UI.SliderLib
{
    public class MusicSlider : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;

        private void OnEnable()
        {
            float volume = SettingsStorage.Instance.GetSettingsData(AudioSetting.Music)._volume;
            SetSliderValue(volume);
        }

        private void SetSliderValue(float volume)
        {
            slider.value = volume;
        }

        public float GetSliderValue()
        {
            return slider.value;
        }
    }
}
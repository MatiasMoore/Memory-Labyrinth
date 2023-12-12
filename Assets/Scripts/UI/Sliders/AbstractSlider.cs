using UnityEngine;

namespace MemoryLabyrinth.UI.SlidersLib
{
    public abstract class AbstractSlider : MonoBehaviour
    {
        public abstract float GetSliderValue();

        public abstract void SetSliderValue(float volume);
    }
}

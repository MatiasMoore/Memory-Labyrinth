using MemoryLabyrinth.Resources;
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.ButtonsLib
{
    public class WinPanelExitButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
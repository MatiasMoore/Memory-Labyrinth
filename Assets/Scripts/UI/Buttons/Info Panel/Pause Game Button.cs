using MemoryLabyrinth.UI.HUD;
using UnityEngine;
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.ButtonsLib
{
    public class PauseGameButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            Time.timeScale = 0f;
            MenuManager.OpenPage(MenuManager.Page.PAUSE);
            Timer.Instance.SetTimerActive(false);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
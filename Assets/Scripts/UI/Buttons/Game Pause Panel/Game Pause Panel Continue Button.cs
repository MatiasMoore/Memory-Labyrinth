using MemoryLabyrinth.UI.HUD;
using UnityEngine;
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class GamePausePanelContinueButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            Time.timeScale = 1f;
            MenuManager.ClosePage(MenuManager.Page.PAUSE);
            Timer.Instance.SetTimerActive(true);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
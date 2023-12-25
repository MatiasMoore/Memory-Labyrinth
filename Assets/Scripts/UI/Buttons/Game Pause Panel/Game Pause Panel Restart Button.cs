using MemoryLabyrinth.Level.Logic;
using UnityEngine;
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.ButtonsLib
{
    public class GamePausePanelRestartButton : Button
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
            LevelManager.Instance.StartCurrentLevelFromSpawn();

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
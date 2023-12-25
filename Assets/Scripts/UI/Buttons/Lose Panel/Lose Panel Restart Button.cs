using MemoryLabyrinth.Level.Logic;
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.ButtonsLib
{
    public class LosePanelRestartButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            MenuManager.ClosePage(MenuManager.Page.LOSE);
            LevelManager.Instance.StartCurrentLevelFromSpawn();

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.ButtonsLib
{
    public class LevelSelectionMenuCloseButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            MenuManager.ClosePage(MenuManager.Page.LEVEL_SELECTION);
            MenuManager.OpenPage(MenuManager.Page.MAIN);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
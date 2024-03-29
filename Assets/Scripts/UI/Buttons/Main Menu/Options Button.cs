using UnityEngine.Events;

namespace MemoryLabyrinth.UI.ButtonsLib
{
    public class OptionsButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            MenuManager.ClosePage(MenuManager.Page.MAIN);
            MenuManager.OpenPage(MenuManager.Page.OPTIONS);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
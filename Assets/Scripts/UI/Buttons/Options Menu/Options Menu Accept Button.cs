using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class OptionsMenuAcceptButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            MenuManager.ClosePage(MenuManager.Page.OPTIONS);
            MenuManager.OpenPage(MenuManager.Page.MAIN);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
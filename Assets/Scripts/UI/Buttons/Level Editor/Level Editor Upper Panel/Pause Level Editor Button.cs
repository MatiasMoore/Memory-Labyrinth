using UnityEngine.Events;

namespace MemoryLabyrinth.UI.ButtonsLib
{
    public class PauseLevelEditorButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            MenuManager.ClosePage(MenuManager.Page.LEVEL_EDITOR_UPPER);
            MenuManager.OpenPage(MenuManager.Page.PAUSE);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
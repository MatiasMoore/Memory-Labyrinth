using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class LevelEditorDeclineLoadButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            MenuManager.ClosePage(MenuManager.Page.LEVEL_EDITOR_LOAD);
            MenuManager.OpenPage(MenuManager.Page.PAUSE);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
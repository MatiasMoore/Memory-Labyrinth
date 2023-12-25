using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class LevelEditorLoadLevelButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            MenuManager.ClosePage(MenuManager.Page.PAUSE);
            MenuManager.OpenPage(MenuManager.Page.LEVEL_EDITOR_LOAD);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
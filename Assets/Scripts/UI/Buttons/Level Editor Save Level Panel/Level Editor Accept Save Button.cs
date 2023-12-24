using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class LevelEditorAcceptSaveButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            // TODO: save level with the specified name
            MenuManager.ClosePage(MenuManager.Page.LEVEL_EDITOR_SAVE);
            MenuManager.OpenPage(MenuManager.Page.PAUSE);
            
            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
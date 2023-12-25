using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class LevelEditorCorrectPathButton : Button
    {
        public override event UnityAction _buttonClick;

        private bool _isClicked = false;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            if (!_isClicked)
            {
                MenuManager.OpenPage(MenuManager.Page.LEVEL_EDITOR_CORRECT_PATH);
                _isClicked = true;
            }
            else
            {
                MenuManager.ClosePage(MenuManager.Page.LEVEL_EDITOR_CORRECT_PATH);
                _isClicked = false;
            }


            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
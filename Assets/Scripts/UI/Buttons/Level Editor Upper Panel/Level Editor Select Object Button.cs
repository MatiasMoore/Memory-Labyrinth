using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class LevelEditorSelectObjectButton : Button
    {
        public override event UnityAction _buttonClick;

        //TEMP
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
                MenuManager.OpenPage(MenuManager.Page.LEVEL_EDITOR_OBJECTS);
                _isClicked = true;
            }
            // TEMP
            else
            {
                MenuManager.ClosePage(MenuManager.Page.LEVEL_EDITOR_OBJECTS);
                _isClicked = false;
            }
            

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
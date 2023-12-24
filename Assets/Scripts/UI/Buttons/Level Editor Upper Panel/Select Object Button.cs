using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class SelectObjectButton : Button
    {
        public override event UnityAction _buttonClick;

        //TEMP
        bool _isClicked = false;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            // TODO: open list with available objects
            if(!_isClicked)
            {
                MenuManager.OpenPage(MenuManager.Page.LEVEL_EDITOR_OBJECTS);
                _isClicked = true;
            }
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
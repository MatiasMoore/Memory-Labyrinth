using MemoryLabyrinth.Resources;
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.ButtonsLib
{
    public class LevelEditorNewLevelButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
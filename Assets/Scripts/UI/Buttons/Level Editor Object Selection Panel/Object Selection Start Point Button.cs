using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class ObjectSelectionStartPointButton : Button
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
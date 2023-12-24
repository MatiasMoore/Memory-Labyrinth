using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class DestroyObjectButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            // TODO: destroy selected object

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class OverviewButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            // TODO: allow camera to move freely

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
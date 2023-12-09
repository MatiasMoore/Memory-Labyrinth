using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class AchievementsButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            MenuManager.ClosePage(MenuManager.Page.MAIN);
            MenuManager.OpenPage(MenuManager.Page.ACHIEVEMENTS);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
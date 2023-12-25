using MemoryLabyrinth.Resources;
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.ButtonsLib
{
    public class LevelEditorButton : Button
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
            ResourceManager.LoadScene(ResourceManager.AvailableScene.LevelEditor);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
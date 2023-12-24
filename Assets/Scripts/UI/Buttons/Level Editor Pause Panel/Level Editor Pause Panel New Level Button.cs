using MemoryLabyrinth.Resources;
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class LevelEditorPausePanelNewLevelButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            // TODO: restart scene/clear all objects from scene
            ResourceManager.LoadScene(ResourceManager.AvailableScene.LevelEditor);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
using MemoryLabyrinth.Level.Logic;
using MemoryLabyrinth.Resources;
using MemoryLabyrinth.SaveLoad;
using UnityEngine;
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class PausePanelExitButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            Time.timeScale = 1f;
            // LevelManager.Instance.SaveUncompleteLevel();
            ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
using UnityEngine;
using UnityEngine.Events;
using MemoryLabyrinth.Resources;
using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.Level.Logic;

namespace MemoryLabyrinth.UI.ButtonsLib
{
    public class LevelSelectButton : Button
    {
        [SerializeField]
        public string _levelName;

        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
            LevelBuilder.Load(_levelName);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
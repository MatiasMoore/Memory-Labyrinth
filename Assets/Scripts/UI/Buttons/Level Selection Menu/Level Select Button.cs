using UnityEngine;
using UnityEngine.Events;
using MemoryLabyrinth.Resources;
using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.Level.Logic;

namespace MemoryLabyrinth.UI.Button
{
    public class LevelSelectButton : Button
    {
        [SerializeField]
        public ResourceManager.Level _level;

        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            // Main logic
            ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
            LevelBuilder.Load(_level);

            // Fire events
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
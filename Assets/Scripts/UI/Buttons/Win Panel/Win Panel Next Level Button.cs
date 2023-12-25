using MemoryLabyrinth.Level.Logic;
using MemoryLabyrinth.Resources;
using MemoryLabyrinth.SaveLoad;
using UnityEngine.Events;

namespace MemoryLabyrinth.UI.Button
{
    public class WinPanelNextLevelButton : Button
    {
        public override event UnityAction _buttonClick;

        public override void FireButtonClickAction()
        {
            _buttonClick?.Invoke();
        }

        public override void OnClick()
        {
            /* Main logic */
            var nextLevel = LevelDataStorage.Instance.GetNextLevelName(LevelBuilder.GetCurrentLevelData()._levelName);
            // If level index is out of enum range, load Main Menu
            if (nextLevel == "")
                ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);
            // Else, load next level
            else
            {
                LevelBuilder.Load(nextLevel);
                LevelManager.Instance.StartCurrentLevelFromSpawn();
                MenuManager.ClosePage(MenuManager.Page.WIN);
            }

            /* Fire events */
            FireButtonClickSoundAction();
            FireButtonClickAction();
        }
    }
}
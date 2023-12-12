using UnityEngine;
using MemoryLabyrinth.Resources;
using MemoryLabyrinth.Level.Logic;

namespace MemoryLabyrinth.SaveLoad
{
    public static class CurrentLevel
    {
        private static LevelData _currentLevel = new LevelData();

        public static void Load(ResourceManager.Level level)
        {
            SaveLoadManager.Instance.LoadGame();

            if (LevelProgressStorage.Instance == null)
            {
                Debug.Log("CURRENTLEVEL: LevelProgressStorage = null");
                return;
            }
            _currentLevel = new LevelData();

            _currentLevel = LevelProgressStorage.Instance.GetLevelInfo(level);
            Debug.Log($"CURRENTLEVEL:{level} loaded");
        }

        public static void Save(LevelData newLevelData)
        {
            _currentLevel = newLevelData;

            if (LevelProgressStorage.Instance == null)
            {
                Debug.Log("CURRENTLEVEL: LevelProgressStorage = null");
                return;
            }

            LevelProgressStorage.Instance.AddLevelInfo(newLevelData);
            Debug.Log($"CURRENTLEVEL: saved {newLevelData._level}");

            SaveLoadManager.Instance.SaveGame();

        }

        public static LevelData GetCurrentLevelData()
        {
            return _currentLevel;
        }

       public static void SetupListeners(LevelModel levelModel)
       {
            levelModel._onLevelWin += Save;
       }

    }
}
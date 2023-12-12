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

        public static void SaveUnfinishedlevel(LevelData newLevelData)
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

        public static void SaveFinishedLevel(LevelData newLevelData)
        {
            if (BonusStorage.Instance == null)
            {
                Debug.Log("CURRENTLEVEL: BonusStorage = null");
                return;
            }

            int bonusSum = 0;
            foreach(BonusInfo bonusInfo in newLevelData._collectedBonuses)
            {
                bonusSum += bonusInfo._value;
            }

            BonusStorage.Instance.EarnBonuses(bonusSum);

            Debug.Log($"CURRENTLEVEL: saved collected bonuses {bonusSum}");

            SaveUnfinishedlevel(newLevelData);

            SaveLoadManager.Instance.SaveGame();

        }

        public static LevelData GetCurrentLevelData()
        {
            return _currentLevel;
        }

       public static void SetupListeners(LevelModel levelModel)
       {
            levelModel._onLevelWin += SaveFinishedLevel;
            levelModel._onPlayerGetCheckpoint += SaveUnfinishedlevel;
       }

    }
}
using UnityEngine;
using MemoryLabyrinth.Resources;
using MemoryLabyrinth.Level.Logic;
using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.Level.Objects.PathLib;
using System.Collections.Generic;
using MemoryLabyrinth.Level.Objects;
using MemoryLabyrinth.Level.Objects.WallLib;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.TeleportLib;
using MemoryLabyrinth.Level.Objects.TrapLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Objects.CorrectPathLib;
using MemoryLabyrinth.SaveLoad.Saveable;

namespace MemoryLabyrinth.Level.Logic
{
    public static class LevelBuilder
    {
        private static LevelParts _levelPartsDB;

        private static LevelProgress _currentLevelProgress = new LevelProgress();
        private static LevelData _currentLevelData = new LevelData();
        private static LevelPartsContainer _currentPartsContainer = new LevelPartsContainer();

        public static void Init(LevelParts levelParts)
        {
            _levelPartsDB = levelParts;
        }

        public static LevelPartsContainer BuildCurrentLevelToScene()
        {
            return BuildLevelToSceneFromData(_currentLevelData);
        }

        private static LevelPartsContainer BuildLevelToSceneFromData(LevelData levelData)
        {
            _currentPartsContainer = new LevelPartsContainer();
            foreach (var levelPart in levelData.parts.parts)
            {
                var prefab = _levelPartsDB.GetConfigByType(levelPart.partType).prefab;
                var createdObj = Object.Instantiate(prefab);
                createdObj.transform.position = levelPart.coords.ToVector3();

                int partId = levelPart.id;
               
                var saveable = createdObj.GetComponent<SaveablePrimitive>();
                if (saveable == null)
                    throw new System.Exception("Object must have saveable primitive");

                saveable.LoadFromString(levelPart.serStr);
                
                _currentPartsContainer.AddPart(new LevelPartsContainer.LevelPartObjectWithType(createdObj, levelPart.partType));
            }
            return _currentPartsContainer;
        }

        public static void Load(string levelName)
        {
            SaveLoadManager.Instance.LoadGame();

            if (LevelProgressStorage.Instance == null)
            {
                Debug.Log("CURRENTLEVEL: LevelProgressStorage = null");
                return;
            }
            _currentLevelProgress = new LevelProgress();

            _currentLevelProgress = LevelProgressStorage.Instance.GetLevelInfo(levelName);

            _currentLevelData = LevelDataStorage.Instance.GetLevelInfo(levelName);

            Debug.Log($"CURRENTLEVEL:{levelName} loaded");
        }

        public static void SaveUnfinishedlevel(LevelProgress newLevelData)
        {
            _currentLevelProgress = newLevelData;

            if (LevelProgressStorage.Instance == null)
            {
                Debug.Log("CURRENTLEVEL: LevelProgressStorage = null");
                return;
            }

            LevelProgressStorage.Instance.AddLevelInfo(newLevelData);
            Debug.Log($"CURRENTLEVEL: saved {newLevelData._levelName}");

            SaveLoadManager.Instance.SaveGame();

        }

        public static void SaveFinishedLevel(LevelProgress newLevelData)
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

        public static LevelProgress GetCurrentLevelData()
        {
            return _currentLevelProgress;
        }

       public static void SetupListeners(LevelModel levelModel)
       {
            levelModel._onLevelWin += SaveFinishedLevel;
            levelModel._onPlayerGetCheckpoint += SaveUnfinishedlevel;
       }

    }
}
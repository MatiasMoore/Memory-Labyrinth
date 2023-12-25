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
using MemoryLabyrinth.Level.Objects.Trap;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.FinishLib;

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

        public static List<GameObject> BuildCurrentLevelToScene()
        {
            return BuildLevelToSceneFromData(_currentLevelData);
        }

        private static List<GameObject> BuildLevelToSceneFromData(LevelData levelData)
        {
            List<GameObject> createdObjs = new List<GameObject>();

            _currentPartsContainer = new LevelPartsContainer();
            foreach (var levelPart in levelData.parts.parts)
            {
                var prefab = _levelPartsDB.GetConfigByType(levelPart.partType).prefab;
                var createdObj = Object.Instantiate(prefab);

                int partId = levelPart.id;
               
                LoadClassDataFromStruct<Path, PathStruct>(createdObj, levelData.paths.paths, partId);
                LoadClassDataFromStruct<Wall, WallStruct>(createdObj, levelData.walls.walls, partId);
                LoadClassDataFromStruct<Bonus, BonusStruct>(createdObj, levelData.bonuses.bonuses, partId);
                LoadClassDataFromStruct<Teleport, TeleportStruct>(createdObj, levelData.teleports.teleports, partId);
                LoadClassDataFromStruct<Trap, TrapStruct>(createdObj, levelData.traps.traps, partId);
                LoadClassDataFromStruct<Checkpoint, CheckpointStruct>(createdObj, levelData.checkPoints.checkPoints, partId);
                LoadClassDataFromStruct<StartPoint, StartPointStruct>(createdObj, levelData.startPoints.startPoints, partId);
                LoadClassDataFromStruct<FinishPoint, FinishPointStruct>(createdObj, levelData.finishPoints.finishPoints, partId);

                _currentPartsContainer.AddPart(new LevelPartsContainer.LevelPartObjectWithType(createdObj, levelPart.partType));
                createdObjs.Add(createdObj);
            }
            return createdObjs;
        }

        private static bool LoadClassDataFromStruct<ClassName, Struct>(GameObject obj, Dictionary<int, Struct> structDict, int id)
        {
            var objComp = obj.GetComponent<ClassName>();

            Struct structFromDict;
            bool structFound = structDict.TryGetValue(id, out structFromDict);

            bool shouldLoadData = objComp != null && structFound;

            if (shouldLoadData)
            {
                var compAsStructable = (IStructable<Struct>)objComp;
                compAsStructable.FromStruct(structFromDict);
            }

            return shouldLoadData;
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
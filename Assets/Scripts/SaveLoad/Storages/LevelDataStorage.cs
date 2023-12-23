using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad
{
    public sealed class LevelDataStorage : MonoBehaviour
    {
        public static LevelDataStorage Instance;

        [SerializeField] private LevelDataListStruct _levels = new LevelDataListStruct();

        public void Init()
        {
            if (Instance != null)
                return;
            _levels.levelDatas = new List<LevelData>();
            Instance = this;
        }

        private bool IsLevelAlreadySaved(string name)
        {
            if (_levels.levelDatas.Count == 0)
            {
                Debug.Log($"LevelDataStorage: level {name} is not saved yet (IsLevelAlreadySaved = false)");
                return false;
            }
            int levelIndex = _levels.levelDatas.FindIndex(item => item.name == name);
            return levelIndex != -1;
        }

        private void CreateLevelInfo(LevelData newLevelData)
        {
            if (!this.IsLevelAlreadySaved(newLevelData.name))
            {
                LevelData _newLevelData = new LevelData();

                _newLevelData.name = newLevelData.name;
                _newLevelData.paths = newLevelData.paths;
                _newLevelData.startPoints = newLevelData.startPoints;
                _newLevelData.teleports = newLevelData.teleports;
                _newLevelData.traps = newLevelData.traps;
                _newLevelData.walls = newLevelData.walls;
                _newLevelData.bonuses = newLevelData.bonuses;
                _newLevelData.checkPoints = newLevelData.checkPoints;
                _newLevelData.finishPoints = newLevelData.finishPoints;


                _levels.levelDatas.Add(_newLevelData);
                Debug.Log($"LevelDataStorage: CreateLevelInfo: Level {newLevelData.name} info has been added");
            }
            else
            {
                Debug.LogWarning($"LevelDataStorage: CreateLevelInfo: Level {newLevelData.name} already exists");
            }
        }

        private void UpdateLevelInfo(LevelData newLevelData)
        {
            if (this.IsLevelAlreadySaved(newLevelData.name))
            {
                int levelIndex = _levels.levelDatas.FindIndex(item => item.name == newLevelData.name);
                LevelData _newLevelData = _levels.levelDatas[levelIndex];

                _newLevelData.name = newLevelData.name;
                _newLevelData.paths = newLevelData.paths;
                _newLevelData.startPoints = newLevelData.startPoints;
                _newLevelData.teleports = newLevelData.teleports;
                _newLevelData.traps = newLevelData.traps;
                _newLevelData.walls = newLevelData.walls;
                _newLevelData.bonuses = newLevelData.bonuses;
                _newLevelData.checkPoints = newLevelData.checkPoints;
                _newLevelData.finishPoints = newLevelData.finishPoints;

                _levels.levelDatas[levelIndex] = _newLevelData;

                Debug.Log($"LevelDataStorage: UpdateLevelInfo: Level {newLevelData.name} info has been updated");
            }
            else
            {
                Debug.LogWarning($"LevelDataStorage: UpdateLevelInfo: Level {newLevelData.name} does not exist");
            }
        }

        public void AddLevelInfo(LevelData levelData)
        {
            if (!this.IsLevelAlreadySaved(levelData.name))
            {
                CreateLevelInfo(levelData);
            }
            else
            {
                UpdateLevelInfo(levelData);
            }
        }

        public void SetLevelDataList(List<LevelData> levelDatas)
        {
            if (levelDatas != null)
                this._levels.levelDatas = levelDatas;
            else
                this._levels.levelDatas = new List<LevelData>(); // TODO: load Scriptable object instead of new list
        }
    }
}
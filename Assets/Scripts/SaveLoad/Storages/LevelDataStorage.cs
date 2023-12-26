using MemoryLabyrinth.Level.Editor;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad
{
    public sealed class LevelDataStorage : MonoBehaviour
    {
        public static LevelDataStorage Instance;

        [SerializeField]
        private LevelDataToFileSaver _defaultLevels;

        [SerializeField] private LevelDataListStruct _levels = new LevelDataListStruct();

        public void Init()
        {
            if (Instance != null)
                return;
            _levels.levelDatas = new List<LevelData>();
            Instance = this;
        }

        [ContextMenu("Clear All Default Levels")]
        private void ClearDefaultLevels()
        {
            _defaultLevels.Clear();
        }

        [ContextMenu("Save All Levels As Default")]
        private void SaveLevelsToFile()
        {
            _defaultLevels.Save(_levels.levelDatas);
            _levels.levelDatas.Clear();
        }

        [ContextMenu("Clear All Saved Levels")]
        private void ClearSavedLevels()
        {
            _levels.levelDatas.Clear();
        }

        public bool IsLevelAlreadySaved(string name)
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
                _levels.levelDatas.Add(newLevelData);
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
                _levels.levelDatas[levelIndex] = newLevelData;

                Debug.Log($"LevelDataStorage: UpdateLevelInfo: Level {newLevelData.name} info has been updated");
            }
            else
            {
                Debug.LogWarning($"LevelDataStorage: UpdateLevelInfo: Level {newLevelData.name} does not exist");
            }
        }

        public string GetNextLevelName(string currentLevelName)
        {
            int currentlevelIndex = _levels.levelDatas.FindIndex(item => item.name == currentLevelName);
            string nextLevelName = "";
            if (currentlevelIndex != _levels.levelDatas.Count - 1)
            {
                nextLevelName = _levels.levelDatas[currentlevelIndex + 1].name;
            }
            return nextLevelName;
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
        public List<LevelData> GetLevelDataList()
        {
            return this._levels.levelDatas;
        }

        public LevelData GetLevelInfo(string name)
        {
            if (this.IsLevelAlreadySaved(name))
            {
                int levelIndex = _levels.levelDatas.FindIndex(item => item.name == name);
                Debug.Log($"GetLevelInfo(): {name}");
                return _levels.levelDatas[levelIndex];
            }
            else
            {
                var newLevelData = new LevelData { name = name };
                Debug.LogWarning($"LevelDataStorage: GetLevelInfo(): {name} does not exist");
                return newLevelData;
            }

        }
        public void SetLevelDataList(List<LevelData> levelDatas)
        {
            _levels = new LevelDataListStruct();
            if (levelDatas != null)
            {
                _levels.levelDatas = levelDatas;
            }
            else
            {
                _defaultLevels.Load(out _levels);
            }
        }
    }
}
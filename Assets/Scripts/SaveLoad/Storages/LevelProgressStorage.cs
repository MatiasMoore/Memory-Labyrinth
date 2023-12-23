using MemoryLabyrinth.Resources;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad
{
    public sealed class LevelProgressStorage : MonoBehaviour
    {
        public static LevelProgressStorage Instance;

        public event Action<LevelProgress> OnLevelProgressChanged;

        [SerializeField] private Levels _levels = new Levels();

        public void Init()
        {
            if (Instance != null)
                return;
            _levels._currentLevels = new List<LevelProgress>();
            Instance = this;
        }

        private void UpdateLevelInfo(LevelProgress newLevelData)
        {
            if (this.IsLevelAlreadySaved(newLevelData._level))
            {
                int levelIndex = _levels._currentLevels.FindIndex(item => item._level == newLevelData._level);
                LevelProgress _newLevelData = _levels._currentLevels[levelIndex];

                _newLevelData._checkpointId = newLevelData._checkpointId;
                _newLevelData._time = newLevelData._time;
                _newLevelData._collectedBonuses = newLevelData._collectedBonuses;
                _newLevelData._livesAmount = newLevelData._livesAmount;
                _newLevelData._isCompleted = newLevelData._isCompleted;

                _levels._currentLevels[levelIndex] = _newLevelData;
                this.OnLevelProgressChanged?.Invoke(newLevelData);
                Debug.Log($"UpdateLevelInfo: Level {newLevelData._level} info has been updated");
            }
            else
            {
                Debug.LogWarning($"UpdateLevelInfo: Level {newLevelData._level} does not exist");
            }
        }

        public LevelProgress GetLevelInfo(ResourceManager.Level level)
        {
            if (this.IsLevelAlreadySaved(level))
            {
                int levelIndex = _levels._currentLevels.FindIndex(item => item._level == level);
                LevelProgress levelData = new LevelProgress
                {
                    _level = level,
                    _checkpointId = _levels._currentLevels[levelIndex]._checkpointId,
                    _collectedBonuses = _levels._currentLevels[levelIndex]._collectedBonuses,
                    _isCompleted = _levels._currentLevels[levelIndex]._isCompleted,
                    _livesAmount = _levels._currentLevels[levelIndex]._livesAmount,
                    _time = _levels._currentLevels[levelIndex]._time
                };
                Debug.Log($"GetLevelInfo(): {level}");
                return levelData;
            }
            else
            {
                var newLevelData = new LevelProgress { _level = level };
                Debug.LogWarning($"GetLevelInfo(): {level} does not exist");
                return newLevelData;
            }

        }

        private void CreateLevelInfo(LevelProgress newLevelData)
        {
            if (!this.IsLevelAlreadySaved(newLevelData._level))
            {
                LevelProgress _newLevelData = new LevelProgress();

                _newLevelData._level = newLevelData._level;
                _newLevelData._checkpointId = newLevelData._checkpointId;
                _newLevelData._time = newLevelData._time;
                _newLevelData._collectedBonuses = newLevelData._collectedBonuses;
                _newLevelData._livesAmount = newLevelData._livesAmount;
                _newLevelData._isCompleted = newLevelData._isCompleted;

                _levels._currentLevels.Add(_newLevelData);
                this.OnLevelProgressChanged?.Invoke(newLevelData);
                Debug.Log($"AddNewLevelInfo: Level {newLevelData._level} info has been added");
            }
            else
            {
                Debug.LogWarning($"AddNewLevelInfo: Level {newLevelData._level} already exists");
            }
        }

        private bool IsLevelAlreadySaved(ResourceManager.Level level)
        {
            if (_levels._currentLevels.Count == 0)
                return false;
            int levelIndex = _levels._currentLevels.FindIndex(item => item._level == level);
            return levelIndex != -1;
        }
        public void AddLevelInfo(LevelProgress levelData)
        {
            if (!this.IsLevelAlreadySaved(levelData._level))
            {
                CreateLevelInfo(levelData);
            }
            else
            {
                UpdateLevelInfo(levelData);
            }
        }

        public List<LevelProgress> GetLevelDataList()
        {
            return this._levels._currentLevels;
        }

        public void SetLevelDataList(List<LevelProgress> levelDatas)
        {
            if (levelDatas != null)
                this._levels._currentLevels = levelDatas;
            else
                this._levels._currentLevels = new List<LevelProgress>();
        }
    }
}
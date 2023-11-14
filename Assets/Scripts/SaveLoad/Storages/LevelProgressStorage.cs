using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class LevelProgressStorage : MonoBehaviour
{
    public static LevelProgressStorage Instance;

    public event Action<LevelData> OnLevelProgressChanged;

    [SerializeField] private List<LevelData> currentLevels { get; set; } = new List<LevelData>();

    public void Init()
    {
        if (Instance != null)
            return;
        this.currentLevels = new List<LevelData>();
        Instance = this;
    }

    private void UpdateLevelInfo(LevelData newLevelData)
    {
        if (this.IsLevelAlreadySaved(newLevelData._level))
        {
            int levelIndex = currentLevels.FindIndex(item => item._level == newLevelData._level);
            LevelData _newLevelData = currentLevels[levelIndex];

            _newLevelData._checkpointId = newLevelData._checkpointId;
            _newLevelData._time = newLevelData._time;
            _newLevelData._collectedBonusesId = newLevelData._collectedBonusesId;
            _newLevelData._livesAmount = newLevelData._livesAmount;
            _newLevelData._isCompleted = newLevelData._isCompleted;

            currentLevels[levelIndex] = _newLevelData;
            this.OnLevelProgressChanged?.Invoke(newLevelData);
            Debug.Log($"UpdateLevelInfo: Level {newLevelData._level} info has been updated");
        }
        else
        {
            Debug.LogWarning($"UpdateLevelInfo: Level {newLevelData._level} does not exist");
        }
    }

    public LevelData GetLevelInfo(ResourceManager.Level level)
    {       
        if (this.IsLevelAlreadySaved(level))
        {
            int levelIndex = currentLevels.FindIndex(item => item._level == level);
            LevelData levelData = new LevelData
            {
                _level = level,
                _checkpointId = currentLevels[levelIndex]._checkpointId,
                _collectedBonusesId = currentLevels[levelIndex]._collectedBonusesId,
                _isCompleted = currentLevels[levelIndex]._isCompleted,
                _livesAmount = currentLevels[levelIndex]._livesAmount,
                _time = currentLevels[levelIndex]._time
            };
            Debug.Log($"GetLevelInfo(): {level}");
            return levelData;
        }
        else throw new System.NullReferenceException($"GetLevelInfo(): {level} does not exist");
    }

    private void CreateLevelInfo(LevelData newLevelData)
    {
        if (!this.IsLevelAlreadySaved(newLevelData._level))
        {
            LevelData _newLevelData = new LevelData();

            _newLevelData._level = newLevelData._level;
            _newLevelData._checkpointId = newLevelData._checkpointId;
            _newLevelData._time = newLevelData._time;
            _newLevelData._collectedBonusesId = newLevelData._collectedBonusesId;
            _newLevelData._livesAmount = newLevelData._livesAmount;
            _newLevelData._isCompleted = newLevelData._isCompleted;

            this.currentLevels.Add(_newLevelData);
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
        if (currentLevels.Count == 0)
            return false;
        int levelIndex = currentLevels.FindIndex(item => item._level == level);
        return levelIndex != -1;
    }
    public void AddLevelInfo(LevelData levelData)
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

    public List<LevelData> GetLevelDataList()
    {
        return this.currentLevels;
    }

    public void SetLevelDataList(List<LevelData> levelDatas)
    {
        if (levelDatas != null)
            this.currentLevels = levelDatas;
    }
}

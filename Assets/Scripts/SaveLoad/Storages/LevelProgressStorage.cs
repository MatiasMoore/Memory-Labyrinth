using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressStorage : MonoBehaviour
{
    public static LevelProgressStorage Instance;

    public event Action<LevelData> OnLevelProgressChanged;

    [SerializeField] public List<LevelData> currentLevels { get; set; } = new List<LevelData>();

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateLevelInfo(LevelData newLevelData)
    {
        int levelIndex = currentLevels.FindIndex(item => item._level == newLevelData._level);
        if (levelIndex != -1)
        {
            LevelData _newLevelData = currentLevels[levelIndex];

            _newLevelData._checkpointId = newLevelData._checkpointId;
            _newLevelData._time = newLevelData._time;
            _newLevelData._collectedBonusesId = newLevelData._collectedBonusesId;
            _newLevelData._livesAmount = newLevelData._livesAmount;
            _newLevelData._isCompleted = newLevelData._isCompleted;

            currentLevels[levelIndex] = _newLevelData;
            Debug.Log($"UpdateLevelInfo: Level {newLevelData._level} info has been updated");
        }
        else
        {
            Debug.LogWarning($"UpdateLevelInfo: Level {newLevelData._level} does not exist");
        }
    }

    public LevelData GetLevelInfo(ResourceManager.Level level)
    {
        int levelIndex = currentLevels.FindIndex(item => item._level == level);
        if (levelIndex != -1)
        {
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

    public void AddNewLevelInfo(LevelData newLevelData)
    {
        int levelIndex = currentLevels.FindIndex(item => item._level == newLevelData._level);
        if (levelIndex == -1)
        {
            LevelData _newLevelData = currentLevels[levelIndex];

            _newLevelData._level = newLevelData._level;
            _newLevelData._checkpointId = newLevelData._checkpointId;
            _newLevelData._time = newLevelData._time;
            _newLevelData._collectedBonusesId = newLevelData._collectedBonusesId;
            _newLevelData._livesAmount = newLevelData._livesAmount;
            _newLevelData._isCompleted = newLevelData._isCompleted;

            currentLevels.Add(_newLevelData);
            Debug.Log($"UpdateLevelInfo: Level {newLevelData._level} info has been added");
        }
        else
        {
            Debug.LogWarning($"UpdateLevelInfo: Level {newLevelData._level} already exists");
        }
    }
}

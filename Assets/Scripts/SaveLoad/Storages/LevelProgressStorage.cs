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

}

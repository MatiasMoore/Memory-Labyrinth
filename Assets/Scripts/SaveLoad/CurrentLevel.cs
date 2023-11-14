using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourceManager;

public static class CurrentLevel
{
    private static LevelData _currentLevel = new LevelData();
    private static int _currentLevelIndex = -1;

    public static void Load(ResourceManager.Level level)
    {
        SaveLoadManager saveLoadManager = new SaveLoadManager();
        saveLoadManager.LoadGame();

        if (LevelProgressStorage.Instance == null)
        {
            Debug.Log("CURRENTLEVEL: LevelProgressStorage = null");
            return;
        }
        _currentLevel = new LevelData
        {
            _level = level
        };

        _currentLevelIndex = LevelProgressStorage.Instance.currentLevels.FindIndex(item => item._level == level);

        if (_currentLevelIndex >= 0)
        {
            _currentLevel = LevelProgressStorage.Instance.currentLevels[_currentLevelIndex];
            Debug.Log($"CURRENTLEVEL:There is a save for {level}");
        }

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
        if (LevelProgressStorage.Instance.currentLevels == null)
        {
            LevelProgressStorage.Instance.currentLevels = new List<LevelData>();    
        }
        _currentLevelIndex = LevelProgressStorage.Instance.currentLevels.FindIndex(item => item._level == newLevelData._level);
        if (_currentLevelIndex < 0)
        {
            Debug.Log($"CURRENTLEVEL: new save for {newLevelData._level} ");
            LevelProgressStorage.Instance.currentLevels.Add(newLevelData);
        } else {
            Debug.Log($"CURRENTLEVEL: {newLevelData._level} save is overriten");
            LevelProgressStorage.Instance.currentLevels[_currentLevelIndex] = newLevelData;
        }

        SaveLoadManager saveLoadManager = new SaveLoadManager();
        saveLoadManager.SaveGame();
        
    }

    public static LevelData getCurrentLevel()
    {
        return _currentLevel;
    }

}

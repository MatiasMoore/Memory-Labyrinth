using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentLevel
{
    private static LevelData _currentLevel = new LevelData();
    private static int _currentLevelIndex;

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
    }

    public static LevelData getCurrentLevel()
    {
        return _currentLevel;
    }

}

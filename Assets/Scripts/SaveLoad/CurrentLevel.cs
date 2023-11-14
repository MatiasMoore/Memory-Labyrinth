using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ResourceManager;

public static class CurrentLevel
{
    private static LevelData _currentLevel = new LevelData();

    public static void Load(ResourceManager.Level level)
    {
        SaveLoadManager saveLoadManager = new SaveLoadManager();
        saveLoadManager.LoadGame();

        if (LevelProgressStorage.Instance == null)
        {
            Debug.Log("CURRENTLEVEL: LevelProgressStorage = null");
            return;
        }
        _currentLevel = new LevelData();

        _currentLevel = LevelProgressStorage.Instance.GetLevelInfo(level);
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

        LevelProgressStorage.Instance.AddLevelInfo(newLevelData);
        Debug.Log($"CURRENTLEVEL: saved {newLevelData._level}");

        SaveLoadManager saveLoadManager = new SaveLoadManager();
        saveLoadManager.SaveGame();
        
    }

    public static LevelData getCurrentLevel()
    {
        return _currentLevel;
    }

}

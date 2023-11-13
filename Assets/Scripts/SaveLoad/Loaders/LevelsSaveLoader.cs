using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LevelData
{
    public ResourceManager.Level _level;
    public int _checkpointId;
    public float _time;
    public List<int> _collectedBonusesId;
    public int _livesAmount;
    public bool _isCompleted;
}
public class LevelsSaveLoader : ISaveLoader
{
    public void LoadData()
    {
        List<LevelData> data = Repository.GetData<List<LevelData>>();
        LevelProgressStorage.Instance.currentLevels = data;
    }

    public void SaveData()
    {
        int amount = BonusStorage.Instance.GetBonuses();
        List<LevelData> levels = LevelProgressStorage.Instance.currentLevels;

        var data = new List<LevelData>(levels);

        Repository.SetData(data);
    }
}

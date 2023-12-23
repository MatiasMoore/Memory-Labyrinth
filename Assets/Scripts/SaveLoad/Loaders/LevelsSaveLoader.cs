using MemoryLabyrinth.Resources;
using System;
using System.Collections.Generic;

namespace MemoryLabyrinth.SaveLoad
{

    [Serializable]
    public struct BonusInfo 
    {
        public int _id;
        public int _value;
    }
    [Serializable]
    public struct LevelProgress
    {
        public ResourceManager.Level _level;
        public int _checkpointId;
        public float _time;
        public List<BonusInfo> _collectedBonuses;
        public int _livesAmount;
        public bool _isCompleted;
    }

    [Serializable]
    public struct Levels
    {
        public List<LevelProgress> _currentLevels;
    }
    public class LevelsSaveLoader : ISaveLoader
    {
        public void LoadData()
        {
            Levels data = Repository.GetData<Levels>();
            LevelProgressStorage.Instance.SetLevelDataList(data._currentLevels);
        }

        public void SaveData()
        {
            List<LevelProgress> levelsList = LevelProgressStorage.Instance.GetLevelDataList();

            var data = new List<LevelProgress>(levelsList);
            Levels levels = new Levels();
            levels._currentLevels = data;

            Repository.SetData(levels);
        }
    }
}
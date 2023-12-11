using MemoryLabyrinth.Resources;
using System;
using System.Collections.Generic;

namespace MemoryLabyrinth.SaveLoad
{
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
            LevelProgressStorage.Instance.SetLevelDataList(data);
        }

        public void SaveData()
        {
            List<LevelData> levels = LevelProgressStorage.Instance.GetLevelDataList();

            var data = new List<LevelData>(levels);

            Repository.SetData(data);
        }
    }
}

using System;
using System.Collections.Generic;
using MemoryLabyrinth.Level.Objects.PathLib;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.TeleportLib;
using MemoryLabyrinth.Level.Objects.Trap;
using MemoryLabyrinth.Level.Objects.WallLib;

namespace MemoryLabyrinth.SaveLoad
{
    // ������� � ����
    [Serializable]
    public struct BonusListStruct
    {
        public List<BonusStruct> bonuses;
    }
    [Serializable]
    public struct WallListStruct
    {
        public List<WallStruct> walls;
    }
    [Serializable]
    public struct PathListStruct
    {
        public List<PathStruct> paths;
    }
    [Serializable]
    public struct TeleportListStruct
    {
        public List<TeleportStruct> teleports;
    }
    [Serializable]
    public struct TrapListStruct
    {
        public List<TrapStruct> traps;
    }
    [Serializable]
    public struct CheckpointListStruct
    {
        public List<CheckpointStruct> checkPoints;
    }
    [Serializable]
    public struct StartPointListStruct
    {
        public List<StartPointStruct> startPoints;
    }
    [Serializable]
    public struct FinishPointListStruct
    {
        public List<FinishPointStruct> finishPoints;
    }
    [Serializable]
    public struct LevelData
    {
        public string name;
        public PathListStruct paths;
        public WallListStruct walls;
        public BonusListStruct bonuses;
        public TeleportListStruct teleports;
        public TrapListStruct traps;
        public CheckpointListStruct checkPoints;
        public StartPointListStruct startPoints;
        public FinishPointListStruct finishPoints;
    }

    [Serializable]
    public struct LevelDataListStruct
    {
        public List<LevelData> levelDatas;
    }

    public class LevelDataSaveLoader : ISaveLoader
    {
        public void LoadData()
        {
            LevelDataListStruct data = Repository.GetData<LevelDataListStruct>();
            LevelDataStorage.Instance.SetLevelDataList(data.levelDatas);
        }

        public void SaveData()
        {
            List<LevelData> levelsList = LevelDataStorage.Instance.GetLevelDataList();

            var data = new List<LevelData>(levelsList);
            LevelDataListStruct levels = new LevelDataListStruct();
            levels.levelDatas = data;

            Repository.SetData(levels);
        }
    }
}
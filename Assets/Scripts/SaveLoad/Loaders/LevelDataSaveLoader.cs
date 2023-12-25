
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
using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.Level.Objects.CorrectPathLib;

namespace MemoryLabyrinth.SaveLoad
{
    // спасибо я поел
    [Serializable]
    public struct BonusListStruct
    {
        public Dictionary<int, BonusStruct> bonuses;
    }
    [Serializable]
    public struct WallListStruct
    {
        public Dictionary<int, WallStruct> walls;
    }
    [Serializable]
    public struct PathListStruct
    {
        public Dictionary<int, PathStruct> paths;
    }
    [Serializable]
    public struct TeleportListStruct
    {
        public Dictionary<int, TeleportStruct> teleports;
    }
    [Serializable]
    public struct TrapListStruct
    {
        public Dictionary<int, TrapStruct> traps;
    }
    [Serializable]
    public struct CheckpointListStruct
    {
        public Dictionary<int, CheckpointStruct> checkPoints;
    }
    [Serializable]
    public struct StartPointListStruct
    {
        public Dictionary<int, StartPointStruct> startPoints;
    }
    [Serializable]
    public struct FinishPointListStruct
    {
        public Dictionary<int, FinishPointStruct> finishPoints;
    }

    [Serializable]
    public struct CorrectPathListStruct
    {
        public Dictionary<int, CorrectPathStruct> correctPathPoints;
    }

    [Serializable]
    public struct LevelPartStruct
    {
        public int id;
        public LevelPartType partType;

        public LevelPartStruct(int newId, LevelPartType newPartType)
        {
            id = newId;
            partType = newPartType;
        }
    }

    [Serializable]
    public struct LevelPartsListStruct
    {
        public List<LevelPartStruct> parts;
    }

    [Serializable]
    public struct LevelData
    {
        public string name;
        public LevelPartsListStruct parts;
        public PathListStruct paths;
        public WallListStruct walls;
        public BonusListStruct bonuses;
        public TeleportListStruct teleports;
        public TrapListStruct traps;
        public CheckpointListStruct checkPoints;
        public StartPointListStruct startPoints;
        public FinishPointListStruct finishPoints;
        public CorrectPathListStruct correctPathPoints;
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
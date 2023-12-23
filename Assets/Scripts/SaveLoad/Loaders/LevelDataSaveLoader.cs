
using System;
using System.Collections.Generic;
using MemoryLabyrinth.Level.Objects.Path;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.TeleportLib;
using MemoryLabyrinth.Level.Objects.Trap;
using MemoryLabyrinth.Level.Objects.Wall;

namespace MemoryLabyrinth.SaveLoad
{
    [Serializable]
    public struct LevelData
    {
        public string name;
        public List<PathStruct> paths;
        public List<WallStruct> walls;
        public List<BonusStruct> bonuses;
        public List<TeleportStruct> teleports;
        public List<TrapStruct> traps;
        public List<CheckpointStruct> checkPoints;
        public List<StartPointStruct> startPoints;
        public List<FinishPointStruct> finishPoints;
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
            //    LevelProgressStorage.Instance.SetLevelDataList(data._currentLevels); TODO as LevelDataSaveLoader
        }

        public void SaveData()
        {
            //    from levelssaveloader
            //    TODO: implement as LevelDataSaveLoader
            //    List<LevelProgress> levelsList = LevelProgressStorage.Instance.GetLevelDataList();

            //    var data = new List<LevelProgress>(levelsList);
            //    Levels levels = new Levels();
            //    levels._currentLevels = data;

            //    Repository.SetData(levels);
        }
    }
}
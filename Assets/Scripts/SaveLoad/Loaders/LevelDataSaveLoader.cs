
using System;
using System.Collections.Generic;
using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.SaveLoad.Saveable;

namespace MemoryLabyrinth.SaveLoad
{
    [Serializable]
    public struct LevelPartStruct
    {
        public int id;
        public LevelPartType partType;
        public Vec3 coords;
        public string serStr;

        public LevelPartStruct(int newId, LevelPartType newPartType, Vec3 newCoords, string newSerStr)
        {
            id = newId;
            partType = newPartType;
            coords = newCoords;
            serStr = newSerStr;
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
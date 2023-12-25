using MemoryLabyrinth.SaveLoad;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    [CreateAssetMenu]
    public class LevelDataListContainer : ScriptableObject
    {
        [SerializeField]
        private string _serStr = "";

        public void Save(List<LevelData> levelsList)
        { 
            var data = new List<LevelData>(levelsList);
            LevelDataListStruct levels = new LevelDataListStruct();
            levels.levelDatas = data;

            _serStr = JsonConvert.SerializeObject(levels);
        }

        public void Load(out LevelDataListStruct strToLoad)
        {
            strToLoad = new LevelDataListStruct();
            strToLoad.levelDatas = new List<LevelData>();
            if (_serStr == "")
                return;

            var deser = JsonConvert.DeserializeObject<LevelDataListStruct>(_serStr);
            strToLoad = deser;
        }

        public void Clear()
        {
            _serStr = "";
        }
    }
}


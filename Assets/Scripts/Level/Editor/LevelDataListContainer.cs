using MemoryLabyrinth.SaveLoad;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    [CreateAssetMenu]
    public class LevelDataToFileSaver : ScriptableObject
    {
        [SerializeField]
        private TextAsset _textAsset;

        public void Save(List<LevelData> levelsList)
        { 
            var data = new List<LevelData>(levelsList);
            LevelDataListStruct levels = new LevelDataListStruct();
            levels.levelDatas = data;

            string serStr = JsonConvert.SerializeObject(levels);
            WriteStrToTextAsset(serStr);
        }

        public void Load(out LevelDataListStruct strToLoad)
        {
            strToLoad = new LevelDataListStruct();
            strToLoad.levelDatas = new List<LevelData>();
            if (_textAsset.text == "")
                return;

            var deser = JsonConvert.DeserializeObject<LevelDataListStruct>(_textAsset.text);
            strToLoad = deser;
        }

        public void Clear()
        {
            WriteStrToTextAsset("");
        }

        private void WriteStrToTextAsset(string str)
        {
            File.WriteAllText(AssetDatabase.GetAssetPath(_textAsset), str);
            EditorUtility.SetDirty(_textAsset);
        }
    }
}


using MemoryLabyrinth.Level.Objects.PathLib;
using Newtonsoft.Json;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad.Saveable
{
    public struct PathSaveableStruct
    {
        public int test;
    }
    public class PathSaveable : SaveablePrimitive
    {
        [SerializeField]
        private Path _path;

        public override bool LoadFromString(string serStr)
        {
            var deserStruct = JsonConvert.DeserializeObject<PathSaveableStruct>(serStr);
            return true;
        }

        public override string SaveToString()
        {
            return JsonConvert.SerializeObject(new PathSaveableStruct { test = 1 });
        }
    }
}
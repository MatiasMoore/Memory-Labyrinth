using Newtonsoft.Json;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad.Saveable
{
    public struct WallSaveableStruct
    {
        public int test;
    }
    public class WallSaveable : SaveablePrimitive
    {
        public override bool LoadFromString(string serStr)
        {
            var deserStruct = JsonConvert.DeserializeObject<WallSaveableStruct>(serStr);
            return true;
        }

        public override string SaveToString()
        {
            return JsonConvert.SerializeObject(new WallSaveableStruct { test = 1 });
        }
    }
}
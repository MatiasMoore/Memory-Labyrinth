using Newtonsoft.Json;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad.Saveable
{
    public struct FinishPointSaveableStruct
    {
        public int test;
    }
    public class FinishPointSaveable : SaveablePrimitive
    { 
        public override bool LoadFromString(string serStr)
        {
            var deserStruct = JsonConvert.DeserializeObject<FinishPointSaveableStruct>(serStr);
            return true;
        }

        public override string SaveToString()
        {
            return JsonConvert.SerializeObject(new FinishPointSaveableStruct { test = 1 });
        }
    }
}
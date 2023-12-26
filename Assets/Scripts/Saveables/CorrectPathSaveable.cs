using Newtonsoft.Json;

namespace MemoryLabyrinth.SaveLoad.Saveable
{
    public struct CorrectPathSaveableStruct
    {
        public int test;
    }
    public class CorrectPathSaveable : SaveablePrimitive
    {
        public override bool LoadFromString(string serStr)
        {
            var deserStruct = JsonConvert.DeserializeObject<CorrectPathSaveableStruct>(serStr);
            return true;
        }

        public override string SaveToString()
        {
            return JsonConvert.SerializeObject(new CorrectPathSaveableStruct { test = 1 });
        }
    }
}
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using Newtonsoft.Json;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad.Saveable
{
    public struct CheckpointSaveableStruct
    {
        public int queue;
    }
    public class CheckpointSaveable : SaveablePrimitive
    {
        [SerializeField]
        private Checkpoint _checkpoint;

        public override bool LoadFromString(string serStr)
        {
            var deserStruct = JsonConvert.DeserializeObject<CheckpointSaveableStruct>(serStr);
            _checkpoint.SetQueue(deserStruct.queue);
            return true;
        }

        public override string SaveToString()
        {
            return JsonConvert.SerializeObject(new CheckpointSaveableStruct { queue = _checkpoint.GetQueue()});
        }
    }
}
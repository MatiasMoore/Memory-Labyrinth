using MemoryLabyrinth.Level.Objects.StartpointLib;
using Newtonsoft.Json;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad.Saveable
{
    public struct StartPointSaveableStruct
    {
        public int queue;
    }
    public class StartPointSaveable : SaveablePrimitive
    {
        [SerializeField]
        private StartPoint _startPoint;

        public override bool LoadFromString(string serStr)
        {
            var deserStruct = JsonConvert.DeserializeObject<StartPointSaveableStruct>(serStr);
            _startPoint.SetQueue(deserStruct.queue);
            return true;
        }

        public override string SaveToString()
        {
            return JsonConvert.SerializeObject(new StartPointSaveableStruct { queue = _startPoint.GetQueue() });
        }
    }
}
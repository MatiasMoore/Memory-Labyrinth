using MemoryLabyrinth.Level.Objects.TeleportLib;
using Newtonsoft.Json;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad.Saveable
{
    public struct TeleportSaveableStruct
    {
        public Vec3 target;
    }
    public class TeleportSaveable : SaveablePrimitive
    {
        [SerializeField]
        private Teleport _teleport;

        public override bool LoadFromString(string serStr)
        {
            var deserStruct = JsonConvert.DeserializeObject<TeleportSaveableStruct>(serStr);
            _teleport.SetTeleportPosition(deserStruct.target.ToVector3());
            return true;
        }

        public override string SaveToString()
        {
            return JsonConvert.SerializeObject(new TeleportSaveableStruct { target = new Vec3(_teleport.GetTeleportPosition()) });
        }
    }
}
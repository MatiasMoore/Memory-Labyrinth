using Newtonsoft.Json;
using UnityEngine;
using MemoryLabyrinth.Level.Objects.TrapLib;

namespace MemoryLabyrinth.SaveLoad.Saveable
{
    public struct TrapSaveableStruct
    {
        public int damage;
    }
    public class TrapSaveable : SaveablePrimitive
    {
        [SerializeField]
        private Trap _trap;

        public override bool LoadFromString(string serStr)
        {
            var deserStruct = JsonConvert.DeserializeObject<TrapSaveableStruct>(serStr);
            _trap.SetDamage(deserStruct.damage);
            return true;
        }

        public override string SaveToString()
        {
            return JsonConvert.SerializeObject(new TrapSaveableStruct { damage = _trap.GetDamage() });
        }
    }
}
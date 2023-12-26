using MemoryLabyrinth.Level.Objects.BonusLib;
using Newtonsoft.Json;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad.Saveable
{ 
    public struct BonusSaveableStruct
    {
        public int id, value;
    }
    public class BonusSaveable : SaveablePrimitive
    {
        [SerializeField]
        private Bonus _bonus;

        public override bool LoadFromString(string serStr)
        {
            var deserStruct = JsonConvert.DeserializeObject<BonusSaveableStruct>(serStr);
            _bonus.SetID(deserStruct.id);
            _bonus.SetValue(deserStruct.value);
            return true;
        }

        public override string SaveToString()
        {
            return JsonConvert.SerializeObject(new BonusSaveableStruct { id = _bonus.GetID(), value = _bonus.GetValue() });
        }
    }
}
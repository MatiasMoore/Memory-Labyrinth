using System;
using System.Collections.Generic;
using UnityEngine;
using static MemoryLabyrinth.Achievemnts.Achievement;

namespace MemoryLabyrinth.Achievemnts
{
    [CreateAssetMenu]
    public class AchievementStrings : ScriptableObject
    {
        [SerializeField]
        public List<AchievementEnumToStr> dict;
    }

    [Serializable]
    public class AchievementEnumToStr
    {
        [SerializeField]
        public AchievmentName enumName;

        [SerializeField]
        public string enumString;
    }
}

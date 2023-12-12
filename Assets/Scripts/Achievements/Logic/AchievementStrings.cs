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

        public string GetNameFromEnum(AchievmentName name)
        {
            foreach (AchievementEnumToStr achievementString in dict)
            {
                if (achievementString.enumName == name)
                    return achievementString.enumString;
            }

            throw new System.Exception("Couldn't find string for achievement enum!");
        }
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

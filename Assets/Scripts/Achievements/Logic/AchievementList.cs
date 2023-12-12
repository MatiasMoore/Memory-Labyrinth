using System;
using System.Collections.Generic;
using UnityEngine;
using static MemoryLabyrinth.Achievemnts.Achievement;

namespace MemoryLabyrinth.Achievemnts
{
    [CreateAssetMenu]
    public class AchievementList : ScriptableObject
    {
        [SerializeField]
        public List<Achievement> list = new List<Achievement>();

        public Achievement GetAchievementByEnum(Achievement.AchievmentName name)
        {
            foreach (Achievement achievement in list)
            {
                if (achievement.GetName().Equals(name))
                    return achievement;
            }

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using static MemoryLabyrinth.Achievements.Achievement;

namespace MemoryLabyrinth.Achievements
{
    [CreateAssetMenu]
    public class AchievementList : ScriptableObject
    {
        [SerializeField]
        private List<Achievement> _list = new List<Achievement>();

        public Achievement GetAchievementByEnum(Achievement.AchievmentName name)
        {
            foreach (Achievement achievement in _list)
            {
                if (achievement.GetName().Equals(name))
                    return new Achievement(achievement);
            }

            return null;
        }

        public List<Achievement> GetAllAchievements() 
        {
            List<Achievement> newList = new List<Achievement>();
            foreach (Achievement achievement in _list)
            {
                newList.Add(new Achievement(achievement));
            }
            return newList;
        }
    }
}

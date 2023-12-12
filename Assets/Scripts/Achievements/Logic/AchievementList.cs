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
        private List<Achievement> _list = new List<Achievement>();

        public Achievement GetAchievementByEnum(Achievement.AchievmentName name)
        {
            foreach (Achievement achievement in _list)
            {
                if (achievement.GetName().Equals(name))
                    return achievement;
            }

            return null;
        }

        public List<Achievement> GetAllAchievements() 
        {
            return this._list;
        }
    }
}

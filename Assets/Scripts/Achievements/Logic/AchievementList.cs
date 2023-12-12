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
    }
}

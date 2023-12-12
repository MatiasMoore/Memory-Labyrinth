using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryLabyrinth.Achievements;

namespace MemoryLabyrinth.SaveLoad
{
    public class AchievementsStorage : MonoBehaviour
    {
        [SerializeField]
        public AchievementList _defaultAchievements;

        public static AchievementsStorage Instance;

        [SerializeField]
        private Achievements _achievements = new Achievements();

        public void Init()
        {
            if (Instance != null)
                return;
            _achievements._achievementsList = new List<AchievementStruct>();
            Instance = this;
        }

        public void SetupAchievementStructList(List<AchievementStruct> achievements)
        {
            if (achievements != null)
            {
                foreach (AchievementStruct achievement in achievements)
                {
                    UpdateAchievementStruct(achievement);
                }
            }
            else
            {
                List<AchievementStruct> defaultStructs = new List<AchievementStruct>();
                foreach (Achievement achievement in _defaultAchievements.GetAllAchievements())
                {
                    defaultStructs.Add(achievement.ToStruct());
                }
                _achievements._achievementsList = defaultStructs;
            }
        }

        public void UpdateAchievementStruct(AchievementStruct achievement)
        {
            int achievementIndex = _achievements._achievementsList.FindIndex(item => item.name == achievement.name);

            if (achievementIndex != -1)
            {
                _achievements._achievementsList[achievementIndex] = achievement;
            }
            else
            {
                _achievements._achievementsList.Add(achievement);
            }
        }

        public void AddAchievementStruct(AchievementStruct achievement)
        {
            int achievementIndex = _achievements._achievementsList.FindIndex(item => item.name == achievement.name);
            if (achievementIndex != -1) _achievements._achievementsList[achievementIndex] = achievement;
            else _achievements._achievementsList.Add(achievement);

        }

        public List<AchievementStruct> GetAchievementStructList()
        {
            return new List<AchievementStruct>(_achievements._achievementsList);
        }

        public AchievementStruct GetAchievementStruct(Achievement.AchievmentName achievmentName)
        {
            int achievementIndex = _achievements._achievementsList.FindIndex(item => item.name == achievmentName);
            if (achievementIndex != -1) return _achievements._achievementsList[achievementIndex];
            else
            {
                Achievement achievement = _defaultAchievements.GetAchievementByEnum(achievmentName);
                Debug.LogWarning($"AchievementsStorage: achievement {achievmentName} not found");
                if (achievement == null) throw new System.Exception($"AchievementsStorage: no default value for achievement {achievmentName}");

                return achievement.ToStruct();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryLabyrinth.Achievemnts;

namespace MemoryLabyrinth.SaveLoad
{
    public class AchievementsStorage : MonoBehaviour
    {
        public static AchievementsStorage Instance;

        private Achievments _achievements = new Achievments();

        public void Init()
        {
            if (Instance != null)
                return;

            Instance = this;
        }

        public void SetupAchievementsList(List<Achievement> achievements)
        {
            if (achievements != null)
            {
                foreach (Achievement achievement in achievements)
                {
                    AchievementsStorage.Instance.SetupAchievement(achievement);
                }
            }
            else
            {
                _achievements._achievementsList = new List<Achievement>();
            }
        }

        public void SetupAchievement(Achievement achievement)
        {

            int achievementIndex = _achievements._achievementsList.FindIndex(item => item.GetName() == achievement.GetName());

            if (achievementIndex != -1)
            {
                _achievements._achievementsList[achievementIndex] = achievement;
            }
            else
            {
                _achievements._achievementsList.Add(achievement);
            }
        }

        public void AddAchievement(Achievement achievement)
        {
            int achievementIndex = _achievements._achievementsList.FindIndex(item => item.GetName() == achievement.GetName());
            if (achievementIndex != -1) _achievements._achievementsList[achievementIndex] = achievement;
            else _achievements._achievementsList.Add(achievement);

        }

        public List<Achievement> GetAchievementsList()
        {
            return this._achievements._achievementsList;
        }

        public Achievement GetAchievement(Achievement.AchievmentName achievmentName)
        {
            int achievementIndex = _achievements._achievementsList.FindIndex(item => item.GetName() == achievmentName);
            if (achievementIndex != -1) return _achievements._achievementsList[achievementIndex];
            else
            {
                Debug.LogError($"AchievementsStorage: achievement {achievmentName} not found");
                return default;
            }
        }
    }
}
using MemoryLabyrinth.Achievements;
using MemoryLabyrinth.SaveLoad;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.UI.Achievements
{
    public class AchievementsController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _parentObject;
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private AchievementStrings _achievementStrings;

        private void Start()
        {
            GenerateAchievementsObjects();
        }

        private List<Achievement> GetAchievementsData()
        {
            List<Achievement> result = new List<Achievement>();
            foreach (AchievementStruct achievementStruct in AchievementsStorage.Instance.GetAchievementStructList())
            {
                result.Add(new Achievement(achievementStruct));
            }
            return result;
        }

        private void SetAchievementData(GameObject achievementObject, Achievement achievement)
        {
            AchievementDisplayingData achievementDisplayingData = achievementObject.GetComponent<AchievementDisplayingData>();
            achievementDisplayingData.SetCompletionStatusIcon(achievement.IsComplete());
            achievementDisplayingData.SetName(_achievementStrings.GetNameFromEnum(achievement.GetName()));
        }

        private void GenerateAchievementsObjects()
        {
            GameObject achievementObject;
            List<Achievement> achievementsData = GetAchievementsData();

            foreach (Achievement achievement in achievementsData)
            {
                achievementObject = Instantiate(_prefab);

                SetAchievementData(achievementObject, achievement);
                achievementObject.transform.parent = _parentObject.transform;
                achievementObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using MemoryLabyrinth.Achievements;


namespace MemoryLabyrinth.SaveLoad
{
    [Serializable]
    public struct Achievements 
    {
        public List<AchievementStruct> _achievementsList;
    }
    public sealed class AchievementsSaveLoader : ISaveLoader
    {
        public void LoadData()
        {
            Achievements data = Repository.GetData<Achievements>();
            AchievementsStorage.Instance.SetupAchievementStructList(data._achievementsList);
        }

        public void SaveData()
        {
            if (AchievementsStorage.Instance == null)
                throw new Exception("AchievementsStorage's instance is null");

            Achievements achievments = new Achievements();
            List<AchievementStruct> data = AchievementsStorage.Instance.GetAchievementStructList();
            achievments._achievementsList = data;

            Repository.SetData(achievments);
        }
    }
}
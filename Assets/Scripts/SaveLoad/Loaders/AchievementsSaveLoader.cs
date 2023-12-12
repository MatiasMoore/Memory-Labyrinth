using System;
using System.Collections.Generic;
using MemoryLabyrinth.Achievemnts;


namespace MemoryLabyrinth.SaveLoad
{
    [Serializable]
    public struct Achievments 
    { 
        public List<Achievement> _achievementsList;
    }
    public sealed class AchievementsSaveLoader : ISaveLoader
    {
        public void LoadData()
        {
            Achievments data = Repository.GetData<Achievments>();
            AchievementsStorage.Instance.SetupAchievementsList(data._achievementsList);
        }

        public void SaveData()
        {
            if (AchievementsStorage.Instance == null)
                return;
            Achievments achievments = new Achievments();
            List<Achievement> data = AchievementsStorage.Instance.GetAchievementsList();
            achievments._achievementsList = data;

            Repository.SetData(achievments);
        }
    }
}
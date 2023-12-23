using MemoryLabyrinth.Level.Logic;
using MemoryLabyrinth.SaveLoad;
using MemoryLabyrinth.UI.HUD;

namespace MemoryLabyrinth.Achievements
{
    public class FinishLevelInSecondsTracker : AchievementTracker
    {
        private LevelModel _levelModel;

        public FinishLevelInSecondsTracker(Achievement achievement, LevelModel levelModel) : base(achievement)
        {
            if (achievement.IsComplete())
                return;

            _levelModel = levelModel;
            levelModel._onLevelWin += LevelFinished;
        }

        private void LevelFinished(LevelProgress levelData)
        {
            if (levelData._time <= _achievement.GetTargetValue())
            {
                _achievement.SetComplete();
                AchievementsStorage.Instance.UpdateAchievementStruct(_achievement.ToStruct());
                SaveLoadManager.Instance.SaveGame();
                _levelModel._onLevelWin -= LevelFinished;
            }
        }
    }
}

using MemoryLabyrinth.Level.Logic;
using MemoryLabyrinth.UI.HUD;

namespace MemoryLabyrinth.Achievemnts
{
    public class FinishLevelInSecondsTracker : AchievementTracker
    {
        private Timer _timer;

        public FinishLevelInSecondsTracker(Achievement achievement, LevelModel levelModel, Timer timer) : base(achievement)
        {
            if (achievement.IsComplete())
                return;

            _timer = timer;
            levelModel._onLevelWin += (LevelData) => LevelFinished();
        }

        private void LevelFinished()
        {
            if (_timer.GetElapsedTime() <= _achievement.GetTargetValue())
                _achievement.SetComplete();
        }
    }
}

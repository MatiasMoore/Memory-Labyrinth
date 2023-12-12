using MemoryLabyrinth.Level.Logic;
using UnityEngine;

namespace MemoryLabyrinth.Achievemnts
{
    public class CollectAllBonussesTracker : AchievementTracker
    {
        private int _totalBonusCountOnLevel;
        private int _currentBonusCount;

        private LevelModel _levelModel;

        public CollectAllBonussesTracker(Achievement achievement, LevelModel levelModel, LevelManager levelManager) : base (achievement)
        {
            if (achievement.IsComplete())
                return;

            _levelModel = levelModel;
            levelModel._onPlayerGetBonus += BonusCollected;
            levelManager._levelStarted += LevelStarted;
            _totalBonusCountOnLevel = GetTotalBonusCount();
        }

        private void BonusCollected()
        {
            _currentBonusCount++;
            Debug.Log("Achievements: bonus count = " + _currentBonusCount);

            if (_currentBonusCount >= _totalBonusCountOnLevel )
                _achievement.SetComplete();
        }

        private void LevelStarted()
        {
            _currentBonusCount = 0;
            _totalBonusCountOnLevel = GetTotalBonusCount();
        }

        private int GetTotalBonusCount()
        {
            return 1;
        }
    }
}
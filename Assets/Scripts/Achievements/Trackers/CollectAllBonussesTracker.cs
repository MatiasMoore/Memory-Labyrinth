using MemoryLabyrinth.Level.Logic;
using UnityEngine;
using MemoryLabyrinth.SaveLoad;
using UnityEngine.Events;

namespace MemoryLabyrinth.Achievements
{
    public class CollectAllBonussesTracker : AchievementTracker
    {
        private int _totalBonusCountOnLevel;
        private int _currentBonusCount;

        private LevelModel _levelModel;

        private LevelManager _levelManager;

        public CollectAllBonussesTracker(Achievement achievement, LevelModel levelModel, LevelManager levelManager) : base (achievement)
        {
            if (achievement.IsComplete())
                return;

            _levelModel = levelModel;
            _levelManager = levelManager;
            levelModel._onPlayerGetBonus += BonusCollected;
            levelManager._levelStarted += LevelStarted;
            _totalBonusCountOnLevel = GetTotalBonusCount();
        }

        private void BonusCollected()
        {
            _currentBonusCount++;
            Debug.Log("Achievements: bonus count = " + _currentBonusCount);

            if (_currentBonusCount >= _totalBonusCountOnLevel)
            {
                _achievement.SetComplete();
                AchievementsStorage.Instance.UpdateAchievementStruct(_achievement.ToStruct());
                SaveLoadManager.Instance.SaveGame();
                _levelModel._onPlayerGetBonus -= BonusCollected;
                _levelManager._levelStarted -= LevelStarted;
            }
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
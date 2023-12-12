using System;
using UnityEngine;
using static MemoryLabyrinth.Achievements.Achievement;

namespace MemoryLabyrinth.Achievements
{
    [Serializable]
    public struct AchievementStruct
    {
        [SerializeField]
        public AchievmentName name;

        [SerializeField]
        public int currentValue;

        [SerializeField]
        public int targetValue;
    }


    [Serializable]
    public class Achievement
    {
        [SerializeField]
        public enum AchievmentName
        {
            collectAllBonusses,
            finishLevelIn10Seconds,
            finishLevelIn20Seconds,
        }

        [SerializeField]
        private AchievmentName _name;

        [SerializeField]
        private int _currentValue;

        [SerializeField]
        private int _targetValue;

        public Achievement(AchievmentName name, int currentValue, int targetValue)
        {
            _name = name;
            _currentValue = currentValue;
            _targetValue = targetValue;
        }

        public Achievement(Achievement achievement) 
        {
            _name = achievement._name;
            _currentValue = achievement._currentValue;
            _targetValue = achievement._targetValue;
        }

        public Achievement(AchievementStruct achievementStruct)
        {
            _name = achievementStruct.name;
            _currentValue = achievementStruct.currentValue;
            _targetValue = achievementStruct.targetValue;
        }

        public AchievementStruct ToStruct()
        {
            return new AchievementStruct { name = _name, currentValue=_currentValue, targetValue=_targetValue };
        }

        public AchievmentName GetName()
        {
            return _name;
        }

        public void SetCurrentValue(int currentValue)
        {
            _currentValue = currentValue;
            if (GetCurrentValue() == GetTargetValue())
                Debug.Log("Achievements: " + GetName().ToString() + " is now complete!");
        }

        public int GetCurrentValue()
        {
            return _currentValue;
        }

        public int GetTargetValue()
        {
            return _targetValue;
        }

        public void SetComplete()
        {
            SetCurrentValue(GetTargetValue());
        }

        public bool IsComplete()
        {
            return GetTargetValue() == GetCurrentValue();
        }
    }
}
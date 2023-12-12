using System;
using UnityEngine;

namespace MemoryLabyrinth.Achievemnts
{
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
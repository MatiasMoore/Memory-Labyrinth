using System;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad
{
    public sealed class BonusStorage : MonoBehaviour
    {
        public static BonusStorage Instance;

        public event Action<int> OnBonusChanged;

        [SerializeField]
        private int currentBonuses;

        public void Init()
        {
            if (Instance != null)
                return;

            Instance = this;
        }

        public void SetupBonuses(int bonuses)
        {
            this.currentBonuses = bonuses;
        }

        public int GetBonuses()
        {
            return this.currentBonuses;
        }

        public void EarnBonuses(int range)
        {
            this.currentBonuses += range;
            this.OnBonusChanged?.Invoke(this.currentBonuses);
        }

        public void SpendBonuses(int range)
        {
            this.currentBonuses -= range;
            this.OnBonusChanged?.Invoke(this.currentBonuses);
        }

    }
}

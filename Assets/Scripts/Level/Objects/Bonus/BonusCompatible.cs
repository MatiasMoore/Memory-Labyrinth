using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.BonusLib
{
    public abstract class BonusCompatible : MonoBehaviour
    {
        public abstract void getBonus(Bonus bonus);
    }
}

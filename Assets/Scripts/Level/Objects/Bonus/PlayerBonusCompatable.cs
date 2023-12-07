using MemoryLabyrinth.Level.Objects.BonusLib;
using UnityEngine;

namespace MemoryLabyrinth.Player
{
    public class PlayerBonusCompatible : BonusCompatible
    {
        public override void getBonus(Bonus bonus)
        {
            MainCharacter mainCharacter = GetComponent<MainCharacter>();
            if (mainCharacter != null)
            {
                mainCharacter.getBonus(bonus);
            }
            else
            {
                Debug.Log($"Bonus {gameObject.name} failed");
            }
        }
    }
}

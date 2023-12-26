using MemoryLabyrinth.Player;
using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.TrapLib
{
    public class PlayerDamage : DamagableObject
    {
        public override void Damage(int damage)
        {
            MainCharacter mainCharacter = GetComponent<MainCharacter>();
            if (mainCharacter != null)
            {
                mainCharacter.getDamage(damage);
            }
            else
            {
                Debug.Log($"Damaging {gameObject.name} failed");
            }
        }
    }
}
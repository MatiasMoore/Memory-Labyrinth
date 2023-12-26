using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.TrapLib
{
    public abstract class DamagableObject : MonoBehaviour
    {
        public abstract void Damage(int damage);
    }
}
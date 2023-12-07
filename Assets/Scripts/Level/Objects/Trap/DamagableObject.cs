using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.Trap
{
    public abstract class DamagableObject : MonoBehaviour
    {
        public abstract void Damage(int damage);
    }
}
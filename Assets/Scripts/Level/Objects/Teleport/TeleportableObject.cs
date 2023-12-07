using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.TeleportLib
{
    public abstract class TeleportableObject : MonoBehaviour
    {
        public abstract bool Teleport(Vector3 position);
    }
}
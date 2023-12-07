using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.FinishLib
{
    public abstract class FinishCompatible : MonoBehaviour
    {
        public abstract void OnFinish(Collider2D collider);
    }
}

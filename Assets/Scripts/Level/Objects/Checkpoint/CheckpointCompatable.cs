using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.CheckpointLib
{
    public abstract class CheckpointCompatable : MonoBehaviour
    {
        public abstract void getCheckpoint(Checkpoint checkpoint);
    }
}

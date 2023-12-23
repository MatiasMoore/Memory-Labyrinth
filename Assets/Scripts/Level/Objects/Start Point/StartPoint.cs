using MemoryLabyrinth.Level.Objects.CheckpointLib;
using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.StartpointLib
{
    public struct StartPointStruct
    {
        Vector3 coords;
    }
    public class StartPoint : Checkpoint
    {
        private Transform _transform;

        public StartPoint() : base(0)
        {
        }
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public Vector3 GetPosition()
        {
            return _transform.position;
        }
    }
}
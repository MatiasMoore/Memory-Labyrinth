using MemoryLabyrinth.Level.Objects.CheckpointLib;
using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.StartpointLib
{
    public struct StartPointStruct
    {
        public Vec3 coords;
    }
    public class StartPoint : Checkpoint, IStructable<StartPointStruct>
    {
        private Transform _transform;

        public StartPoint() : base(0)
        {
        }

        public StartPointStruct ToStruct()
        {
            return new StartPointStruct { coords = new Vec3(_transform.position) };
        }

        public void FromStruct(StartPointStruct str)
        {
            transform.position = str.coords.ToVector3();
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
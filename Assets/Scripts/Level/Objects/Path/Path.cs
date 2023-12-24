using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.PathLib
{
    public struct PathStruct
    {
        public Vec3 coords;
    }

    public class Path : MonoBehaviour, IStructable<PathStruct>
    {
        public PathStruct ToStruct()
        {
            return new PathStruct() { coords = new Vec3(transform.position) };
        }
    }
   
}


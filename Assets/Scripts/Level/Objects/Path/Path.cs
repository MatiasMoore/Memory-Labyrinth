using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.PathLib
{
    public struct PathStruct
    {
        public Vector3 coords;
    }

    public class Path : MonoBehaviour, IStructable<PathStruct>
    {
        public PathStruct ToStruct()
        {
            return new PathStruct() { coords = transform.position };
        }
    }
   
}


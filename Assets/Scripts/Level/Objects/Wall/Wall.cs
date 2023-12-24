using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.WallLib
{
    public class Wall : MonoBehaviour, IStructable<WallStruct>
    {
        public WallStruct ToStruct()
        {
            return new WallStruct() { coords = transform.position };
        }
    }

    public struct WallStruct
    {
        public Vector3 coords;
    }
}

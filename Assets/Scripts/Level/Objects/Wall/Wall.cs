using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.Wall
{
    public class Wall : MonoBehaviour
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

using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.WallLib
{
    public class Wall : MonoBehaviour, IStructable<WallStruct>
    {
        public WallStruct ToStruct()
        {
            return new WallStruct() { coords = new Vec3(transform.position) };
        }

        public void FromStruct(WallStruct str)
        {
            transform.position = str.coords.ToVector3();
        }
    }

    public struct WallStruct
    {
        public Vec3 coords;
    }
}

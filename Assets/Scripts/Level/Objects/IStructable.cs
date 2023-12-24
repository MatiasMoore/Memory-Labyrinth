using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.WallLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Objects
{
    public struct Vec3
    {
        public float x, y, z;

        public Vec3(Vector3 unityVector)
        {
            x = unityVector.x; 
            y = unityVector.y; 
            z = unityVector.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }

    public interface IStructable<T>
    {
        public T ToStruct();

        public void FromStruct(T str);
    }
}
using System;
using UnityEngine;

namespace MemoryLabyrinth.SaveLoad.Saveable
{
    [Serializable]
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

    public abstract class SaveablePrimitive : MonoBehaviour
    {
        public abstract string SaveToString();

        public abstract bool LoadFromString(string serStr);

    }
}
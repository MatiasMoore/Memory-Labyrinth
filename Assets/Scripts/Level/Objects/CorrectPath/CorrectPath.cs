using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.CorrectPathLib
{
    [SerializeField]
    public struct CorrectPathStruct
    {
        public Vec3 position;
    }

    public class CorrectPath : MonoBehaviour, IStructable<CorrectPathStruct>
    {
        public void FromStruct(CorrectPathStruct str)
        {
            transform.position = str.position.ToVector3();
        }

        public CorrectPathStruct ToStruct()
        {
            return new CorrectPathStruct { position = new Vec3(transform.position) };
        }
    }
}


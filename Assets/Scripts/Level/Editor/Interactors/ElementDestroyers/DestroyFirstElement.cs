using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.Level.Objects.CorrectPathLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class DestroyFirstElement : ElementDestroyerPrimitive
    {
        public DestroyFirstElement(LevelPartsContainer container) : base(container)
        {
        }

        public override InteractorPrimitive InteractAtPos(Vector2 pos)
        {
            List<GameObject> objects = _container.GetObjectsAtPos(pos);
            foreach (GameObject obj in objects)
            {
                if (obj.GetComponent<CorrectPath>() == null)
                {
                    DestroyElement(obj);
                    return GetInteractorToReturn();
                }
            }

            return GetInteractorToReturn();
        }
    }
}


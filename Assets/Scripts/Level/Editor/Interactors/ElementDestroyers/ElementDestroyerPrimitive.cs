using MemoryLabyrinth.Level.Editor;
using System.Collections;
using System.Collections.Generic;
using MemoryLabyrinth.Utils;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public abstract class ElementDestroyerPrimitive : InteractorPrimitive
    {

        protected LevelPartsContainer _container;

        public ElementDestroyerPrimitive(LevelPartsContainer container)
        {
            _container = container;
        }

        public void DestroyAtPos(Vector2 pos) {
            List<GameObject> posObjects = _container.GetObjectsAtPos(pos);
            if (posObjects.Count > 0)
            {
                DestroyElement(posObjects[0]);
                
            }
        }

        public void DestroyElement(GameObject element)
        {
            _container.DeletePart(element);
            Object.Destroy(element);
        }

        public InteractorPrimitive GetInteractorToReturn()
        {
            return this;
        }
    }
}


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
            List<GameObject> posObjects = LevelUtils.GetAllObjectsAtPos(pos);
            if (posObjects.Count > 0)
            {
                GameObject destroyableObject = posObjects[0];
                _container.DeletePart(destroyableObject);
                Object.Destroy(destroyableObject);
            }
        }
    }
}


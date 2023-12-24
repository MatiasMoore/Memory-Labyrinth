using MemoryLabyrinth.Level.Editor;
using System.Collections;
using System.Collections.Generic;
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
            List<GameObject> posObjects = GetAllObjectsAtPos(pos);
            if (posObjects.Count > 0)
            {
                GameObject destroyableObject = posObjects[0];
                _container.DeletePart(destroyableObject);
                Object.Destroy(destroyableObject);
            }
        }

        public List<GameObject> GetAllObjectsAtPos(Vector2 position)
        {
            var colls = Physics2D.OverlapPointAll(position);
            List<GameObject> objects = new List<GameObject>();
            foreach (var coll in colls)
            {
                objects.Add(coll.gameObject);
            }
            return objects;
        }

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Utils
{
    public static class LevelUtils
    {
        static public List<GameObject> GetAllObjectsAtPos(Vector2 position)
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


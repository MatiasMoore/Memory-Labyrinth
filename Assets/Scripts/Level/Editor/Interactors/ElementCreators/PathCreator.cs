using MemoryLabyrinth.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class PathCreator : ElementCreatorPrimitive
    {
        public PathCreator(LevelPartsContainer container, LevelPartConfig config) : base(container, config)
        {
        }

        public override bool CanBePlacedAtPos(Vector2 position)
        {
            var objsAtPos = _container.GetObjectsAtPos(position);

            bool noObjects = objsAtPos.Count == 0;
            
            return noObjects;
        }
    }
}


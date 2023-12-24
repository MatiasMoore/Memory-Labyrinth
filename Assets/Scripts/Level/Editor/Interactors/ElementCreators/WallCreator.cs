using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.Level.Objects.PathLib;
using System.Collections;
using System.Collections.Generic;
using MemoryLabyrinth.Utils;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class WallCreator : ElementCreatorPrimitive
    {
        public WallCreator(LevelPartsContainer container, LevelPartConfig config) : base(container, config)
        {
            objectPlaced += (GameObject obj) => DeletePathAtPos(obj.transform.position);
        }

        private void DeletePathAtPos(Vector2 position)
        {
            var objsAtPos = LevelUtils.GetAllObjectsAtPos(position);
            foreach (var obj in objsAtPos)
            {
                if (obj.GetComponent<Path>() != null)
                {
                    _container.DeletePart(obj);
                    Object.Destroy(obj);
                }
            }
        }

        public override bool CanBePlacedAtPos(Vector2 position)
        {
            var objsAtPos = LevelUtils.GetAllObjectsAtPos(position);

            bool noObjects = objsAtPos.Count == 0;
            bool onlyOneObject = objsAtPos.Count == 1;
            bool isFirstObjPath = !noObjects && objsAtPos[0].GetComponent<Path>() != null;

            return noObjects || (onlyOneObject && isFirstObjPath);
        }
    }
}


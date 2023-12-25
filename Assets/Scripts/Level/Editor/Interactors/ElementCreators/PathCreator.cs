using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Objects.PathLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.TeleportLib;
using MemoryLabyrinth.Level.Objects.Trap;
using MemoryLabyrinth.Level.Objects.WallLib;
using MemoryLabyrinth.Utils;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class PathCreator : ElementCreatorPrimitive
    {
        public PathCreator(LevelPartsContainer container, LevelPartConfig config) : base(container, config)
        {
            objectPlaced += DeleteWall;
        }

        private void DeleteWall(GameObject obj)
        {
            foreach (var element in _container.GetObjectsAtPos(obj.transform.position))
            {
                if (element.GetComponent<Wall>() != null)
                {
                    _container.DeletePart(element);
                    Object.Destroy(element);
                }
            }
        }

        public override bool CanBePlacedAtPos(Vector2 position)
        {
            var objsAtPos = _container.GetObjectsAtPos(position);

            bool noObjects = objsAtPos.Count == 0;
            bool isWallPresent = _container.ContainsPartAtPos<Wall>(position);
          
            return noObjects || isWallPresent;
        }
    }
}


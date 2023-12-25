using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Objects.PathLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MemoryLabyrinth.Level.Editor.LevelPartsContainer;

namespace MemoryLabyrinth.Level.Editor
{
    public class StartPointCreator : ElementCreatorPrimitive
    {
        public StartPointCreator(LevelPartsContainer container, LevelPartConfig config) : base(container, config)
        {
        }

        public override bool CanBePlacedAtPos(Vector2 position)
        {

            bool isPathPresent = _container.ContainsPartAtPos<Path>(position);
            bool isFinishPointNotPresent = !_container.ContainsPartAtPos<FinishPoint>(position);
            bool isStartPointNotPresent = !_container.ContainsPartAtPos<StartPoint>(position);
            bool isCheckpointNotPresent = !_container.ContainsPartAtPos<Checkpoint>(position);

            return isPathPresent && isFinishPointNotPresent && isStartPointNotPresent && isCheckpointNotPresent;
        }
    }

}

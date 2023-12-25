using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.CorrectPathLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Objects.PathLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.TeleportLib;
using MemoryLabyrinth.Level.Objects.Trap;
using MemoryLabyrinth.Level.Objects.WallLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectPathCreator : ElementCreatorPrimitive
{
    public CorrectPathCreator(LevelPartsContainer container, LevelPartConfig config) : base(container, config)
    {
    }

    public override bool CanBePlacedAtPos(Vector2 position)
    {
        var objsAtPos = _container.GetObjectsAtPos(position);

        bool noObjects = objsAtPos.Count == 0;
        bool isPathPresent = _container.ContainsPartAtPos<Path>(position);
        bool isWallPresent = _container.ContainsPartAtPos<Wall>(position);
        bool isTrapPresent = _container.ContainsPartAtPos<Trap>(position);
        bool isBonusPresent = _container.ContainsPartAtPos<Bonus>(position);
        bool isFinishPresent = _container.ContainsPartAtPos<FinishPoint>(position);
        bool isCheckpointPresent = _container.ContainsPartAtPos<Checkpoint>(position);
        bool isStartPresent = _container.ContainsPartAtPos<StartPoint>(position);
        bool isTeleportPresent = _container.ContainsPartAtPos<Teleport>(position);
        bool isCorrectPathPresent = _container.ContainsPartAtPos<CorrectPath>(position);

        return !noObjects && !isCorrectPathPresent;
    }
}

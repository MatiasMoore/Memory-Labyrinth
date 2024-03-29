using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.Level.Objects.CorrectPathLib;
using MemoryLabyrinth.Level.Objects.FinishLib;
using MemoryLabyrinth.Level.Objects.PathLib;
using MemoryLabyrinth.Level.Objects.StartpointLib;
using MemoryLabyrinth.Level.Objects.TeleportLib;
using MemoryLabyrinth.Level.Objects.TrapLib;
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

        if(noObjects || isCorrectPathPresent)
        {
            return false;
        }

        List<CorrectPath> correctPaths = _container.GetPartsOfType<CorrectPath>();
        if (correctPaths.Count == 0)
        {
            return true;
        }

        CorrectPath lastCorrectPath = correctPaths[correctPaths.Count - 1];
        bool isLastCorrectPathUp = (Vector2)lastCorrectPath.transform.position == position + Vector2.up;
        bool isLastCorrectPathDown = (Vector2)lastCorrectPath.transform.position == position + Vector2.down;
        bool isLastCorrectPathLeft = (Vector2)lastCorrectPath.transform.position == position + Vector2.left;
        bool isLastCorrectPathRight = (Vector2)lastCorrectPath.transform.position == position + Vector2.right;

        return isLastCorrectPathUp || isLastCorrectPathDown || isLastCorrectPathLeft || isLastCorrectPathRight;
    }
}

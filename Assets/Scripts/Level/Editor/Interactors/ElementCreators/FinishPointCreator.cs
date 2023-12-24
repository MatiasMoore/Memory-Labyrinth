using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class FinishPointCreator : ElementCreatorPrimitive
    {
        public FinishPointCreator(LevelPartsContainer container, LevelPartConfig config) : base(container, config)
        {
        }

        public override bool CanBePlacedAtPos(Vector2 position)
        {
            return true;
        }
    }

}

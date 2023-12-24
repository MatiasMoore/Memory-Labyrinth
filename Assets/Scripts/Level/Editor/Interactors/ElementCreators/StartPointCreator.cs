using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class StartPointCreator : ElementCreatorPrimitive
    {
        public StartPointCreator(LevelPartsContainer container, LevelPartConfig config) : base(container, config)
        {
        }

        public override bool CanBePlacedAtPos(Vector2 position)
        {
            return true;
        }
    }

}

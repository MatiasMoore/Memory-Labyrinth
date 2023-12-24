using MemoryLabyrinth.Level.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class DestroyFirstElement : ElementDestroyerPrimitive
    {
        public DestroyFirstElement(LevelPartsContainer container) : base(container)
        {
        }

        public override void InteractAtPos(Vector2 pos)
        {
            DestroyAtPos(pos);
        }
    }
}


using MemoryLabyrinth.Level.Editor;
using MemoryLabyrinth.Level.Objects.CorrectPathLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class CorrectPathDestroyer : ElementDestroyerPrimitive
    {
        public CorrectPathDestroyer(LevelPartsContainer container) : base(container)
        {
        }

        public override void InteractAtPos(Vector2 pos)
        {
            List<CorrectPath> correctPaths = _container.GetPartsOfType<CorrectPath>();

            if (correctPaths.Count == 0)
            {
                return;
            }

            if ((Vector2)correctPaths.Last().transform.position == pos)
            {
                DestroyAtPos(pos);
            }

        }
    }
}


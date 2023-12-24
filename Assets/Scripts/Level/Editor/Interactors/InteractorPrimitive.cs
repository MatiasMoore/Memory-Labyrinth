using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public abstract class InteractorPrimitive 
    { 
        public abstract void InteractAtPos(Vector2 pos);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public abstract class InteractorPrimitive 
    { 
        public abstract InteractorPrimitive InteractAtPos(Vector2 pos);
    }
}


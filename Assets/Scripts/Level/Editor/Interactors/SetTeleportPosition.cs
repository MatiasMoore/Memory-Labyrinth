using MemoryLabyrinth.Level.Objects.TeleportLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryLabyrinth.Level.Editor
{
    public class SetTeleportPosition : InteractorPrimitive
    {
        Teleport _teleport;
        InteractorPrimitive _lastInteractor;
        public SetTeleportPosition(Teleport teleport, InteractorPrimitive lastInteractor)
        {
            _teleport = teleport;
            _lastInteractor = lastInteractor;
        }
        public override InteractorPrimitive InteractAtPos(Vector2 pos)
        {
            if (_teleport == null)
                throw new SystemException("SetTeleportPosition: teleport null!");

            Debug.Log($"SetTeleportPosition: Position set to{pos}");
            _teleport.SetTeleportPosition(pos);
            return _lastInteractor;
        }
    }

}

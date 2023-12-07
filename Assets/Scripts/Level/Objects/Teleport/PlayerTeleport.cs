using MemoryLabyrinth.Level.Objects.TeleportLib;
using UnityEngine;
using UnityEngine.Events;

namespace MemoryLabyrinth.Player
{
    public class PlayerTeleport : TeleportableObject
    {
        public event UnityAction _teleportEvent;
        public override bool Teleport(Vector3 position)
        {
            GetComponent<MainCharacter>().TeleportTo(position);
            _teleportEvent?.Invoke();
            return true;
        }
    }
}
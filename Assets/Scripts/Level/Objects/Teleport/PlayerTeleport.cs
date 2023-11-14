using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

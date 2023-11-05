using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : TeleportableObject
{
    public override bool Teleport(Vector3 position)
    {
        GetComponent<MainCharacter>().TeleportTo(position);
        return true;
    }
}

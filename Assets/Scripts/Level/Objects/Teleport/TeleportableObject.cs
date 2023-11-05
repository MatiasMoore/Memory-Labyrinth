using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TeleportableObject : MonoBehaviour
{
    public abstract bool Teleport(Vector3 position);
}

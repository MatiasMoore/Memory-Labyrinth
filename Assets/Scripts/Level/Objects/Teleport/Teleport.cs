using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Teleport : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    public void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TeleportableObject teleportableObject = other.gameObject.GetComponent<TeleportableObject>();
        if (teleportableObject != null)
        {
            Debug.Log($"Teleporting {other.gameObject.name} to {_target.position}");
            teleportableObject.Teleport(_target.position);
        }
        else
        {
            Debug.Log($"Teleporting {other.gameObject.name} failed");
        }
    }
}

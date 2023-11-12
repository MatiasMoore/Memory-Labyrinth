using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class Teleport : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    public void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        float thickness = 0.3f;
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, _target.position);
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

    public void DisableTeleportPath()
    {
        GetComponent<LineRenderer>().enabled = false;
    }
}

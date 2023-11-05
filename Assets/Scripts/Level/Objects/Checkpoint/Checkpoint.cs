using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private int _queue;

    public void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckpointCompatable checkpointObject = other.gameObject.GetComponent<CheckpointCompatable>();
        if (checkpointObject != null)
        {
            Debug.Log($"Checkpoint {other.gameObject.name} for {_queue}");
            checkpointObject.getCheckpoint(this);

            // Change color to yellow
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            Debug.Log($"Checkpoint {other.gameObject.name} failed");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Finish : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<Collider2D> _finishEvent;

    public void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _finishEvent.Invoke(other);
    }

    public void AddOnTriggerAction(UnityAction<Collider2D> action)
    {
        _finishEvent.AddListener(action);
    }
}

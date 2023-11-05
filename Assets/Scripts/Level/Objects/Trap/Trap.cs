using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Trap : MonoBehaviour
{
    [SerializeField]
    private int _damage = 1; 

    public void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamagableObject damagableObject = other.gameObject.GetComponent<DamagableObject>();
        if (damagableObject != null)
        {
            Debug.Log($"Damaging {other.gameObject.name} for {_damage} damage");
            damagableObject.Damage(_damage);
        }
        else
        {
            Debug.Log($"Damaging {other.gameObject.name} failed");
        }
    }

}

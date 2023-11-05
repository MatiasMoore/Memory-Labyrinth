using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bonus : MonoBehaviour
{
    int _value = 10;

    public void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BonusCompatable bonusObject = other.gameObject.GetComponent<BonusCompatable>();
        if (bonusObject != null)
        {
            Debug.Log($"Bonus {other.gameObject.name} for {_value}");
            bonusObject.getBonus(_value);
            //Удалить себя
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"Bonus {other.gameObject.name} failed");
        }
    }
}

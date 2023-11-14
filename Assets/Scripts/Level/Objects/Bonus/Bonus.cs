using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bonus : MonoBehaviour
{
    int _value = 10;
    int _id;

    public void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BonusCompatible bonusObject = other.gameObject.GetComponent<BonusCompatible>();
        if (bonusObject != null)
        {
            Debug.Log($"Bonus {other.gameObject.name} for {_value}");
            bonusObject.getBonus(this);
            //Удалить себя
            DestroySelf();
        }
        else
        {
            Debug.Log($"Bonus {other.gameObject.name} failed");
        }
    }

    public int GetID()
    {
        return _id;
    }

    public int GetValue()
    {
        return _value;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

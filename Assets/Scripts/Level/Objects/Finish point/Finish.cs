using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Finish : MonoBehaviour
{
    public void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FinishCompatible finishObject =
            other.gameObject.GetComponent<FinishCompatible>();
        if (finishObject != null)
        {
            Debug.Log($"Finish {other.gameObject.name}");
            finishObject.OnFinish(other);
        }
        else
        {
            Debug.Log($"{other.gameObject.name} can't finish");
        }   
    }
}

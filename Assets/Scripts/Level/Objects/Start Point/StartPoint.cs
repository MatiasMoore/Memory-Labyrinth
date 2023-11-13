using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Transform _transform;
    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    public Vector3 GetPosition()
    {
        return _transform.position;
    }
}

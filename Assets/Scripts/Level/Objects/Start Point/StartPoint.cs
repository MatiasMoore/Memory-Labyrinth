using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : Checkpoint
{
    private Transform _transform;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public Vector3 GetPosition()
    {
        return _transform.position;
    }
}

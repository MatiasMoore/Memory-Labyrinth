using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float _speed;
    private ObjectMovement _objectMovement; 

    void Start()
    {
        _objectMovement = new ObjectMovement(GetComponent<Transform>(), GetComponent<Rigidbody2D>(), _speed);
    }

    void FixedUpdate()
    {
        _objectMovement.UpdatePosition(Time.fixedDeltaTime);
    }

    public void FollowPath(List<Vector3> path)
    {
        _objectMovement.FollowPath(path);
    }

    public void StopMoving()
    {
        _objectMovement.StopMove();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public List<Vector2> _debugPath;

    [SerializeField] private float _speed;
    private ObjectMovement _objectMovement; 

    void Start()
    {
        _objectMovement = new ObjectMovement(GetComponent<Transform>(), GetComponent<Rigidbody2D>(), _speed);

        _objectMovement.FollowPath(_debugPath);
    }

    void FixedUpdate()
    {
        _objectMovement.UpdatePosition(Time.deltaTime);
    }

    public void FollowPath(List<Vector2> path)
    {
        _objectMovement.FollowPath(path);
    }

    public void StopMoving()
    {
        _objectMovement.StopMove();
    }
}

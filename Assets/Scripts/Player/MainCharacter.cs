using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float _speed;

    private ObjectMovement _objectMovement; 
    private PathCreator _pathCreator;

    void Start()
    {
        _objectMovement = new ObjectMovement(GetComponent<Transform>(), GetComponent<Rigidbody2D>(), _speed);
        _pathCreator = GetComponent<PathCreator>();
    }

    void FixedUpdate()
    {
        _objectMovement.UpdatePosition(Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (_pathCreator.isNewPathReady)
        {
            FollowPath(_pathCreator.GetNewPath());
        }
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

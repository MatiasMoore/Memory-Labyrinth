using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    [SerializeField] public ObjectMovementState.State _currentState = ObjectMovementState.State.STAY;
    private ObjectMovementState _objectMovement; 
    private PathCreator _pathCreator;

    void Start()
    {
        _objectMovement = new ObjectMovementState(GetComponent<Transform>(), GetComponent<Rigidbody2D>(), _speed);
        _pathCreator = GetComponent<PathCreator>();
    }

    void FixedUpdate()
    {
        _objectMovement.Update(Time.fixedDeltaTime);
        _currentState = _objectMovement.GetState();
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

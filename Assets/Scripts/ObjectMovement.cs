using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class ObjectMovement
{


    private Transform _transform;
    private Rigidbody2D _rigidbody;
    [SerializeField] private List<Vector2> _path;
    private float _speed;

    private int _currentPositionIndex;
    private bool _isPathComplete = true;
    private bool _isMoving;


    private Vector2 _currentDirection;
    public ObjectMovement(Transform transform, Rigidbody2D rigidbody, float speed)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        _speed = speed;
    }
    private Vector2 GetDirectionTo(Vector2 point)
    {
        if (_transform.position.x < point.x)
        {
            Debug.Log("Moving right");
            return Vector2.right;
        }
        if (_transform.position.x > point.x)
        {
            Debug.Log("Moving left");
            return Vector2.left;
        }

        if (_transform.position.y > point.y)
        {
            Debug.Log("Moving down");
            return Vector2.down;
        }

        if (_transform.position.y < point.y)
        {
            Debug.Log("Moving up");
            return Vector2.up;
        }

        Debug.Log("Stay");
        return Vector2.zero;
    }
    private void MoveToNextPoint()
    {
        if (_path.Count - 1 > _currentPositionIndex)
        {
            Debug.Log($"Start moving from ({_transform.position.x}, {_transform.position.y}) to ({_path[_currentPositionIndex + 1].x}, {_path[_currentPositionIndex + 1].y})");
            
            _currentPositionIndex++;
            _currentDirection = GetDirectionTo(_path[_currentPositionIndex]);
            _isMoving = true;
        } else
        {
            Vector2 _curentPosition = _transform.position;
            if (_curentPosition == _path.Last())
            {
                _isPathComplete = true;

                Debug.Log("Path complete!");
            }

        }
    }
    private void Movement()
    {    
        Vector2 velocity =  _currentDirection * _speed;
        _rigidbody.velocity = velocity;  
    }
    private void CheckEndOfTheMovement()
    {
        Vector2 _curentPosition = _transform.position;
        if (_curentPosition == _path[_currentPositionIndex])
        {
            _rigidbody.velocity = Vector2.zero;
            _transform.position = _path[_currentPositionIndex];
            _isMoving = false;

            Debug.Log("Point reached");
        }
    }
    public void FollowPath(List<Vector2> path)
    {
        _path = new List<Vector2>(path);
        _isPathComplete = false;
        _currentPositionIndex = -1;
        MoveToNextPoint();
    }
    public void StopMove()
    {

        _path.RemoveRange(_currentPositionIndex+1, _path.Count - _currentPositionIndex - 1);

    }
    public void UpdatePosition()
    {
        if (_isPathComplete)
        {
            return;
        }

        if (_isMoving)
        {
            Movement();
            CheckEndOfTheMovement();

        } else
        {
            MoveToNextPoint();
        }

    }


}

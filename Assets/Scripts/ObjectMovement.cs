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

    private Vector2 _currentPosition;
    private float _acceptableError = 0.001f;
    private float _timeToPassPoint;
    private float _timer;
    public ObjectMovement(Transform transform, Rigidbody2D rigidbody, float speed)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        _speed = speed;
        _timeToPassPoint = 1 / speed;
    }
    private void MoveToNextPoint()
    {
        if (_path.Count - 1 > _currentPositionIndex)
        {
            Debug.Log($"Start moving from ({_transform.position.x}, {_transform.position.y}) to ({_path[_currentPositionIndex + 1].x}, {_path[_currentPositionIndex + 1].y})");

            _timer = 0;
            _currentPosition = _transform.position;
            _currentPositionIndex++;
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
    private bool IsStayOnPoint(Vector2 point)
    {
        return (Mathf.Abs(_transform.position.x - point.x) < _acceptableError && Mathf.Abs(_transform.position.y - point.y) < _acceptableError);
    }
    private void Movement(float timeFromLastFrame)
    {
        float t = _timer / _timeToPassPoint;
        Vector2 newPosition = Vector2.Lerp(_currentPosition, _path[_currentPositionIndex],  (t*t)*(3-2*t));
        Vector2 velocity = (newPosition - (Vector2)_transform.position) / timeFromLastFrame;
        _rigidbody.velocity = velocity;
        _timer += timeFromLastFrame;
    }
    private void CheckEndOfTheMovement()
    {
        if (IsStayOnPoint(_path[_currentPositionIndex]))
        {
            _rigidbody.velocity = Vector2.zero;
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
    public void UpdatePosition(float timeFromLastFrame)
    {
        if (_isPathComplete)
        {
            return;
        }

        if (_isMoving)
        {
            Movement(timeFromLastFrame);
            CheckEndOfTheMovement();

        } else
        {
            MoveToNextPoint();
        }
    }
}

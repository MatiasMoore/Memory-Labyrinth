using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ObjectMovement
{
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private List<Vector2> _path = new List<Vector2>();
    private float _timeToPassPoint;

    private int _currentPositionIndex = 0;
    private bool _isPathComplete = true;
    private Vector2 _currentPosition;
    private float _acceptableError = 0.01f;
    private float _timer;
    public ObjectMovement(Transform transform, Rigidbody2D rigidbody, float speed)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        _timeToPassPoint = 1 / speed;
    }

    private float GetInterpolationFunction()
    {
        float t = _timer / _timeToPassPoint;
        if (_path.Count == 1)
        {
            Debug.Log("Smooth step");
            return (t * t) * (3 - 2 * t); // Smooth step 
        }
        else if (_currentPositionIndex == _path.Count - 1)
        {
            Debug.Log("Deceleration");
            return Mathf.Sqrt(t); // Deceleration 
        }
        else if (_currentPositionIndex == 0)
        {
            Debug.Log("Acceleration");
            return t * t * 0.9f; // Acceleration 
        }
        else
        {
            Debug.Log("Linear");
            return t;
        }
    }
    private void MoveToNextPoint()
    {
        if (_path.Count - 1 > _currentPositionIndex)
        {
            Debug.Log($"Start moving from ({_transform.position.x}, {_transform.position.y}) to ({_path[_currentPositionIndex + 1].x}, {_path[_currentPositionIndex + 1].y})");

            _timer = 0;
            _currentPosition = _transform.position;
            _currentPositionIndex++;
        }
        else
        {
            _isPathComplete = true;
            _rigidbody.velocity = Vector2.zero;
            Debug.Log("Path complete!");
        }
    }
    private bool IsStayOnPoint()
    {

        return (Mathf.Abs(_transform.position.x - _path[_currentPositionIndex].x) < _acceptableError) && (Mathf.Abs(_transform.position.y - _path[_currentPositionIndex].y) < _acceptableError);
    }
    private void Movement(float timeFromLastFrame)
    {
        Vector2 newPosition = Vector2.Lerp(_currentPosition, _path[_currentPositionIndex], GetInterpolationFunction());
        Vector2 velocity = (newPosition - (Vector2)_transform.position) / timeFromLastFrame;
        _rigidbody.velocity = velocity;
        Debug.Log($"velocity{velocity}");
        _timer += timeFromLastFrame;
    }
    public void FollowPath(List<Vector3> path)
    {
        Vector2 currentPosition = _transform.position;
        if (_path.Count > 0)
        {
            currentPosition = _path[_currentPositionIndex];
        }
        _path = new List<Vector2>();
        foreach (var item in path)
        {
            _path.Add((Vector2)item);
        } 
        _path.Insert(0, currentPosition);
        _isPathComplete = false;
        _currentPositionIndex = 0;
        MoveToNextPoint();
    }
    public void StopMove()
    {
        _path.RemoveRange(_currentPositionIndex + 1, _path.Count - _currentPositionIndex - 1);
        _currentPosition = _transform.position;
        _timer = 0;
    }
    public void UpdatePosition(float timeFromLastFrame)
    {
        if (_isPathComplete)
        {
            return;
        }
        if (IsStayOnPoint())
        {
            MoveToNextPoint();
        }
        Movement(timeFromLastFrame);        
    }
}

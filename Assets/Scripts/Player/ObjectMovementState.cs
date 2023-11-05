using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectMovementState
{
    public enum State
    {
        STAY,
        SMOOTHSTEP,
        ACCELERATION,
        DECELERATION,
        LINEAR,
        TELEPORT
    }

    private State _currentState;

    private Transform _transform;

    private Rigidbody2D _rigidbody;

    private List<Vector2> _path;

    private float _timeToPassPoint;

    private Vector2 _currentPosition;

    private float _timer;

    private readonly float _acceptableError = 0.01f;

    private readonly int _cellToAccelerate = 1;

    private readonly int _cellToDecelerate = 1;

    public ObjectMovementState(Transform transform, Rigidbody2D rigidbody, float speed)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        _timeToPassPoint = 1 / speed;

        _currentState = State.STAY;
        _path = new List<Vector2>();
    }

    public void Update(float deltaTime)
    {
        switch (_currentState)
        {
            case State.STAY:
                Stay(deltaTime);
                break;
            case State.ACCELERATION:
                Acceleration(deltaTime);
                break;
            case State.LINEAR:
                Linear(deltaTime);
                break;
            case State.DECELERATION:
                Deceleration(deltaTime);
                break;
            case State.SMOOTHSTEP:
                SmoothStep(deltaTime);
                break;
            case State.TELEPORT:
                Teleport(deltaTime);
                break;
        }
    }

    //Функция может принимать как Vector2, так и Vector3

    public void FollowPath(List<Vector2> path)
    {
        _path = new List<Vector2>(path);
        _timer = 0;
        _currentPosition = _transform.position;
        SwitchStateTo(State.ACCELERATION);
    }

    public void FollowPath(List<Vector3> path)
    {
        _path = new List<Vector2>();
        foreach (var point in path)
        {
            _path.Add((Vector2)point);
        }
        _timer = 0;
        _currentPosition = _transform.position;
        SwitchStateTo(State.ACCELERATION);
    }

    public void StopMove()
    {
        if (_path.Count > 1)
        {
            _path.RemoveRange(1, _path.Count - 1);
        }
    }

    public void TeleportTo(Vector2 position)
    {
        StopMove();
        _path.Add(position);
        SwitchStateTo(State.TELEPORT);
        Debug.Log($"PLAYER: Teleporting to {_path[1]}");
    }

    public State GetState()
    {
        return _currentState;
    }

    private void Linear(float deltaTime)
    {
        UpdatePosition(InterpolateByState(_currentState, _timeToPassPoint, deltaTime));

        if (IsStayOnPoint(_path[0]))
        {
            RemoveReachedPointFromPath(_path[0]);

            if (IsDeceleration())
                SwitchStateTo(State.DECELERATION);
        }
    }

    private void Acceleration(float deltaTime)
    {
        UpdatePosition(InterpolateByState(_currentState, _timeToPassPoint, deltaTime));

        if (IsStayOnPoint(_path[0]))
        {
            RemoveReachedPointFromPath(_path[0]);

            if (IsStay())
                SwitchStateTo(State.STAY);
            if (IsDeceleration())
                SwitchStateTo(State.DECELERATION);
            if (IsLinear())
                SwitchStateTo(State.LINEAR);
        }
    }

    private void Deceleration(float deltaTime)
    {
        UpdatePosition(InterpolateByState(_currentState, _timeToPassPoint, deltaTime));

        if (IsStayOnPoint(_path[0]))
        {
            RemoveReachedPointFromPath(_path[0]);

            if (IsStay())
                SwitchStateTo(State.STAY);
        }
    }

    private void SmoothStep(float deltaTime)
    {
        UpdatePosition(InterpolateByState(_currentState, _timeToPassPoint, deltaTime));

        if (IsStayOnPoint(_path[0]))
        {
            RemoveReachedPointFromPath(_path[0]);

            if (IsStay())
                SwitchStateTo(State.STAY);
        }
    }

    private void Stay(float deltaTime)
    {
        if (IsSmoothStep())
            SwitchStateTo(State.SMOOTHSTEP);
        if (IsAcceleration())
            SwitchStateTo(State.ACCELERATION);
    }

    private void Teleport(float deltaTime)
    {
        UpdatePosition(InterpolateByState(State.DECELERATION, _timeToPassPoint, deltaTime));

        if (IsStayOnPoint(_path[0]))
        {
            RemoveReachedPointFromPath(_path[0]);
            _transform.position = new Vector3(_path[0].x, _path[0].y, _transform.position.z);
            RemoveReachedPointFromPath(_path[0]);
            SwitchStateTo(State.STAY);
        }
    }

    private void UpdatePosition(float positionStage)
    {
        Vector2 newPosition = Vector2.Lerp(_currentPosition, _path[0], positionStage);
        _rigidbody.MovePosition(newPosition);
    }

    private float InterpolateByState(State state, float time, float deltaTime)
    {
        _timer += deltaTime;
        float t = _timer / time;
        return state switch
        {
            State.STAY => 0,
            State.ACCELERATION => t * t, // Acceleration
            State.LINEAR => t,
            State.DECELERATION => Mathf.Sqrt(t), // Deceleration
            State.SMOOTHSTEP => t * t * (3 - 2 * t), // Smooth step
            _ => throw new System.Exception($"Unknown state to interpolate: {_currentState}"),
        };
    }

    private void RemoveReachedPointFromPath(Vector2 point)
    {
        _currentPosition = point;
        _path.Remove(point);
        _timer = 0;
    }

    private bool IsStayOnPoint(Vector2 currentPosition)
    {
        return (Mathf.Abs(_transform.position.x - currentPosition.x) < _acceptableError)
            && (Mathf.Abs(_transform.position.y - currentPosition.y) < _acceptableError);
    }

    private bool IsSmoothStep()
    {
        return _path.Count == 1;
    }

    private bool IsDeceleration()
    {
        return _path.Count <= _cellToDecelerate && _path.Count >= 1;
    }

    private bool IsAcceleration()
    {
        return _path.Count <= _cellToAccelerate && _path.Count >= 1;
    }

    private bool IsStay()
    {
        return _path.Count == 0;
    }

    private bool IsLinear()
    {
        return !IsSmoothStep() && !IsDeceleration() && !IsAcceleration() && !IsStay();
    }

    private void SwitchStateTo(State newState)
    {
        Debug.Log(
            $"PLAYER: Switching from {_currentState} to {newState}, Path length: {_path.Count}"
        );
        _currentState = newState;
    }
}

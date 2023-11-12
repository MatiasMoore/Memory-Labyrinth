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
        Stay,
        SmoothStep,
        Acceleration,
        Deceleration,
        Linear,
        Teleport,
        Turn
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

    private readonly float _decelerationTimeOffset = 1.4f;

    private readonly float _accelerationTimeOffset = 1.4f;

    public ObjectMovementState(Transform transform, Rigidbody2D rigidbody, float speed)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        _timeToPassPoint = 1 / speed;

        _currentState = State.Stay;
        _path = new List<Vector2>();
    }

    public void Update(float deltaTime)
    {
        switch (_currentState)
        {
            case State.Stay:
                Stay(deltaTime);
                break;
            case State.Acceleration:
                Acceleration(deltaTime);
                break;
            case State.Linear:
                Linear(deltaTime);
                break;
            case State.Deceleration:
                Deceleration(deltaTime);
                break;
            case State.SmoothStep:
                SmoothStep(deltaTime);
                break;
            case State.Teleport:
                Teleport(deltaTime);
                break;
            case State.Turn:
                Turn(deltaTime);
                break;
        }
    }

    //Функция может принимать как Vector2, так и Vector3

    public void FollowPath(List<Vector2> path)
    {
        path.RemoveRange(0, 1);
        _path = new List<Vector2>(path);
        _timer = 0;
        _currentPosition = _transform.position;
        if (IsSmoothStep())
        {
            SwitchStateTo(State.SmoothStep);
        }
        else
        {
            if (IsAcceleration() && !isTurn())
                SwitchStateTo(State.Acceleration);
            else
                SwitchStateTo(State.Turn);
        }
    }

    public void FollowPath(List<Vector3> path)
    {
        path.RemoveRange(0, 1);
        _path = new List<Vector2>();
        foreach (var point in path)
        {
            _path.Add((Vector2)point);
        }
        _timer = 0;
        _currentPosition = _transform.position;
        if (IsSmoothStep())
        {
            SwitchStateTo(State.SmoothStep);
        }
        else
        {
            if (IsAcceleration() && !isTurn())
                SwitchStateTo(State.Acceleration);
            else
                SwitchStateTo(State.Turn);
        }
    }

    public void StopMove()
    {
        if (GetState() == State.Teleport)
        {
            Debug.Log("Can't stop while teleporting");
            return;
        }

        if (_path.Count > 1)
        {
            _path.RemoveRange(1, _path.Count - 1);
            _timer = 0;
            _currentPosition = _transform.position;
            SwitchStateTo(State.Deceleration);
        }
    }

    public void TeleportTo(Vector2 position)
    {
        StopMove();
        _path.Add(position);
        SwitchStateTo(State.Teleport);
        Debug.Log($"PLAYER: Teleporting to {_path[1]}");
    }

    public State GetState()
    {
        return _currentState;
    }

    private void Linear(float deltaTime)
    {
        if (_timer == 0)
            _currentPosition = (Vector2)_transform.position;

        UpdatePosition(InterpolateByState(_currentState, _timeToPassPoint, deltaTime), true);

        if (_timer > _timeToPassPoint)
        {
            RemoveReachedPointFromPath(_path[0]);

            if (IsDeceleration())
                SwitchStateTo(State.Deceleration);
            if (IsStay())
                SwitchStateTo(State.Stay);
            if (isTurn())
                SwitchStateTo(State.Turn);
        }
    }

    private void Acceleration(float deltaTime)
    {
        if (_timer == 0)
            _currentPosition = (Vector2)_transform.position;

        UpdatePosition(InterpolateByState(_currentState, _timeToPassPoint * _accelerationTimeOffset, deltaTime), true);

        if (_timer > _timeToPassPoint * _accelerationTimeOffset)
        {
            RemoveReachedPointFromPath(_path[0]);

            if (IsStay())
                SwitchStateTo(State.Stay);
            if (IsDeceleration())
                SwitchStateTo(State.Deceleration);
            if (IsLinear())
                SwitchStateTo(State.Linear);
            if (isTurn())
                SwitchStateTo(State.Turn);
        }
    }

    private void Deceleration(float deltaTime)
    {
        if (_timer == 0)
            _currentPosition = (Vector2)_transform.position;

        UpdatePosition(InterpolateByState(_currentState, _timeToPassPoint * _decelerationTimeOffset,deltaTime),false);

        if (_timer > _timeToPassPoint * _decelerationTimeOffset)
        {
            RemoveReachedPointFromPath(_path[0]);

            if (IsStay())
                SwitchStateTo(State.Stay);
        }
    }

    private void SmoothStep(float deltaTime)
    {
        if (_timer == 0)
            _currentPosition = (Vector2)_transform.position;

        UpdatePosition(InterpolateByState(_currentState, _timeToPassPoint * _accelerationTimeOffset * _decelerationTimeOffset, deltaTime), false);

        if (_timer > _timeToPassPoint * _accelerationTimeOffset * _decelerationTimeOffset)
        {
            RemoveReachedPointFromPath(_path[0]);

            if (IsStay())
                SwitchStateTo(State.Stay);
        }
    }

    private void Stay(float deltaTime)
    {
        if (IsSmoothStep())
            SwitchStateTo(State.SmoothStep);
        if (IsAcceleration())
            SwitchStateTo(State.Acceleration);
    }

    private void Teleport(float deltaTime)
    {
        if (_timer == 0)
            _currentPosition = (Vector2)_transform.position;

        UpdatePosition(InterpolateByState(State.Deceleration, _timeToPassPoint, deltaTime), false);

        if (_timer > _timeToPassPoint)
        {
            RemoveReachedPointFromPath(_path[0]);
            _transform.position = new Vector3(_path[0].x, _path[0].y, _transform.position.z);
            RemoveReachedPointFromPath(_path[0]);
            SwitchStateTo(State.Stay);
        }
    }

    private void Turn(float deltaTime)
    {
        if (_timer == 0)
            _currentPosition = (Vector2)_transform.position;

        UpdatePosition(InterpolateByState(State.Linear, _timeToPassPoint, deltaTime), false);

        if (_timer > _timeToPassPoint)
        {
            RemoveReachedPointFromPath(_path[0]);
            if (IsLinear())
                SwitchStateTo(State.Linear);
            if (IsDeceleration())
                SwitchStateTo(State.Deceleration);
            if (IsStay())
                SwitchStateTo(State.Stay);
        }
    }

    private void UpdatePosition(float positionStage, bool isUnClamped)
    {
        Vector2 newPosition;
        if (isUnClamped)
            newPosition = Vector2.LerpUnclamped(_currentPosition, _path[0], positionStage);
        else
            newPosition = Vector2.Lerp(_currentPosition, _path[0], positionStage);

        // Debug.Log(
        //     $"PLAYER: current position: {_currentPosition}, transform {(Vector2)_transform.position}, newPosition: {newPosition}, delta: {(Vector2)_transform.position - newPosition}, target position: {_path[0]}, stage: {positionStage}, isUnClamped: {isUnClamped}"
        // );
        _rigidbody.MovePosition(newPosition);
    }

    private float InterpolateByState(State state, float time, float deltaTime)
    {
        _timer += deltaTime;
        float t = _timer / time;
        return state switch
        {
            State.Stay => 0,
            State.Acceleration => 1 - Mathf.Cos((t * Mathf.PI) / 2), // Acceleration
            State.Linear => t,
            State.Deceleration => 1 - (1 - t) * (1 - t), // Deceleration
            State.SmoothStep => t < 0.5 ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2, // Smooth step
            _ => throw new System.Exception($"Unknown state to interpolate: {_currentState}"),
        };
    }

    private void RemoveReachedPointFromPath(Vector2 point)
    {
        Debug.Log($"PLAYER: removepoint: transform {(Vector2)_transform.position}, point {point}");
        _path.Remove(point);
        _timer = 0;
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
        return _path.Count >= _cellToAccelerate;
    }

    private bool IsStay()
    {
        return _path.Count == 0;
    }

    private bool isTurn()
    {
        if (_path.Count < 2)
            return false;
        Vector2 currentDirection = _path[0] - _currentPosition;
        currentDirection.Normalize();
        Vector2 nextDirection = _path[1] - _currentPosition;
        nextDirection.Normalize();

        return Vector2.Dot(currentDirection, nextDirection) <= 1 - _acceptableError;
    }

    private bool IsLinear()
    {
        return !IsSmoothStep() && !IsDeceleration() && !IsStay() && !isTurn();
    }

    private void SwitchStateTo(State newState)
    {
        Debug.Log($"PLAYER: Switching from {_currentState} to {newState}, Path length: {_path.Count}");
        _currentState = newState;
    }
}

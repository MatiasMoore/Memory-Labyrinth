using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    private int _health;
    public event UnityAction _onDeathEvent;
    public event UnityAction _onDamageEvent;
    public event UnityAction<int> _onBonusEvent;
    public event UnityAction _onFinishEvent;
    public event UnityAction _onTeleportEvent;
    public event UnityAction<Checkpoint> _onCheckpointEvent;

    [SerializeField]
    private float _speed;

    [SerializeField]
    public ObjectMovementState.State _currentState;
    private ObjectMovementState _objectMovement;
    private PathCreator _pathCreator;
    private bool _isActive;

    public void Init()
    {
        TouchControls.Instance.addCallbackToTouchDown(StopMovingOnTouch);
        _objectMovement = new ObjectMovementState(
            GetComponent<Transform>(),
            GetComponent<Rigidbody2D>(),
            _speed
        );
        _pathCreator = GetComponent<PathCreator>();
        _pathCreator.Init();
    }

    void FixedUpdate()
    {
        _objectMovement.Update(Time.fixedDeltaTime);
        _currentState = _objectMovement.GetState();
    }

    private void Update()
    {   
        if (_isActive)
        {
            if (_pathCreator.isNewPathReady)
            {
                FollowPath(_pathCreator.GetNewPath());
            }
            _pathCreator.SetActive(_currentState == ObjectMovementState.State.Stay);
        }
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
        _pathCreator.SetActive(isActive);
    }

    public void FollowPath(List<Vector3> path)
    {
        _objectMovement.FollowPath(path);
    }


    private void StopMovingOnTouch(InputAction.CallbackContext context)
    {
        if (_currentState != ObjectMovementState.State.Stay)
        {
            _objectMovement.StopMove();
        }
    }

    public void StopMoving()
    {
        _objectMovement.StopMove();
    }

    public void TeleportTo(Vector3 position)
    {
        _objectMovement.TeleportTo((Vector2)position);
        _onTeleportEvent?.Invoke();
    }

    public void getDamage(int damage)
    {
        _health -= damage;
        _onDamageEvent.Invoke();
        if (_health <= 0)
        {
            _onDeathEvent.Invoke();
        }
    }

    public void getBonus(int bonus)
    {
        _onBonusEvent.Invoke(bonus);
    }

    public void getCheckpoint(Checkpoint checkpoint)
    {
        _onCheckpointEvent.Invoke(checkpoint);
    }

    public void Finish()
    {
        _objectMovement.StopMove();
        _onFinishEvent.Invoke();
    }
}

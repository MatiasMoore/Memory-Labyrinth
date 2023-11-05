using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    private int _health;

    [SerializeField]
    private UnityEvent _onDeathEvent;

    [SerializeField]
    private UnityEvent _onDamageEvent;

    [SerializeField]
    private float _speed;

    [SerializeField]
    public ObjectMovementState.State _currentState = ObjectMovementState.State.STAY;
    private ObjectMovementState _objectMovement;
    private PathCreator _pathCreator;

    void Start()
    {
        _objectMovement = new ObjectMovementState(
            GetComponent<Transform>(),
            GetComponent<Rigidbody2D>(),
            _speed
        );
        _pathCreator = GetComponent<PathCreator>();

        // added on death event to level manager
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

    public void TeleportTo(Vector3 position)
    {
        _objectMovement.TeleportTo((Vector2)position);
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

    public void AddOnDeathAction(UnityAction listener)
    {
        _onDeathEvent.AddListener(listener);
    }

    public void AddOnDamageAction(UnityAction listener)
    {
        _onDamageEvent.AddListener(listener);
    }
}

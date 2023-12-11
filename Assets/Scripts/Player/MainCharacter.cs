using MemoryLabyrinth.Controls;
using MemoryLabyrinth.Level.Objects.BonusLib;
using MemoryLabyrinth.Level.Objects.CheckpointLib;
using MemoryLabyrinth.ObjectMovement;
using MemoryLabyrinth.Path;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MemoryLabyrinth.Player
{
    public class MainCharacter : MonoBehaviour
    {
        [SerializeField]
        private int _maxHealth = 3;

        [SerializeField]
        private int _health;

        public int GetHealth()
        {
            return _health;
        }

        public event UnityAction _onDeathEvent;
        public event UnityAction _onDamageEvent;
        public event UnityAction<Bonus> _onBonusEvent;
        public event UnityAction _onFinishEvent;
        public event UnityAction _onTeleportEvent;
        public event UnityAction<Checkpoint> _onCheckpointEvent;
        public event UnityAction<int> _onPlayerHealthChangedEvent;

        [SerializeField]
        private float _speed;

        [SerializeField]
        public ObjectMovementState.State _currentState;
        private ObjectMovementState _objectMovement;
        private PathCreator _pathCreator;
        private bool _isActive;

        public MainCharacter(float speed, int maxHealth, int health)
        {
            _speed = speed;
            _maxHealth = maxHealth;
            _health = health;
        }

        public void Init()
        {
            if (TouchControls.Instance == null)
            {
                Debug.Log("TouchControls is not initialized");
            }
            else
            {
                TouchControls.Instance.addCallbackToTouchDown(StopMovingOnTouch);
            }
            _objectMovement = new ObjectMovementState(
                GetComponent<Transform>(),
                GetComponent<Rigidbody2D>(),
                _speed
            );
            _isActive = true;
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

        public void SetPosition2d(Vector2 position)
        {
            _objectMovement.TeleportTo(position);
        }

        public void TeleportTo(Vector3 position)
        {
            _objectMovement.TeleportTo((Vector2)position);
            _onTeleportEvent?.Invoke();
        }

        public void getDamage(int damage)
        {
            SetHealth(_health - damage);
            _onDamageEvent?.Invoke();
            if (_health <= 0)
            {
                _onDeathEvent?.Invoke();
            }
        }

        public void getBonus(Bonus bonus)
        {
            _onBonusEvent?.Invoke(bonus);
        }

        public void getCheckpoint(Checkpoint checkpoint)
        {
            _onCheckpointEvent?.Invoke(checkpoint);
        }

        public void Finish()
        {
            _objectMovement.StopMove();
            _onFinishEvent?.Invoke();
        }

        public void SetHealth(int health)
        {
            if (health > _maxHealth)
            {
                health = _maxHealth;
            }

            if (health <= 0)
            {
                health = 0;
            }

            _health = health;
            _onPlayerHealthChangedEvent?.Invoke(_health);
        }
        public void ResetHealth()
        {
            SetHealth(_maxHealth);
        }

        public void SetMaxHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
        }


    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MemoryLabyrinth.Controls
{
    [RequireComponent(typeof(PlayerInput))]
    public class TouchControls : MonoBehaviour
    {
        private const float timeToRegisterTouchHold = 0.2f;

        //Class with the control map
        private PlayerInput _playerInput;

        //Main actions
        private InputAction _touchPositionAction;
        private InputAction _touchPressAction;

        public event UnityAction touchDown;
        public event UnityAction touchUp;
        private Coroutine _touchHoldCoroutine;
        private float _touchHoldTime;
        public event UnityAction touchHold;

        public static TouchControls Instance { get; private set; }

        public void Init()
        {
            if (Instance != null) return;

            _playerInput = GetComponent<PlayerInput>();
            _touchPressAction = _playerInput.actions["TouchPress"];
            _touchPositionAction = _playerInput.actions["TouchPosition"];

            _touchPressAction.performed += FireTouchDownEvent;
            _touchPressAction.canceled += FireTouchUpEvent;

            Instance = this;
        }

        private void OnDisable()
        {
            _touchPressAction.performed -= FireTouchDownEvent;
            _touchPressAction.canceled -= FireTouchUpEvent;
            StopAllCoroutines();
        }

        private void FireTouchDownEvent(InputAction.CallbackContext context)
        {
            touchDown?.Invoke();
            _touchHoldCoroutine = StartCoroutine(FireTouchHoldEvent());
        }

        private void FireTouchUpEvent(InputAction.CallbackContext context)
        {
            touchUp?.Invoke();
            if (_touchHoldCoroutine != null)
            {
                StopCoroutine(_touchHoldCoroutine);
                _touchHoldCoroutine = null;
            }
        }

        private IEnumerator FireTouchHoldEvent()
        {
            _touchHoldTime = 0;
            while (true)
            {
                if (_touchHoldTime >= timeToRegisterTouchHold)
                    touchHold?.Invoke();

                _touchHoldTime += Time.deltaTime;
                yield return null;
            }
        }

        public Vector2 getTouchScreenPosition()
        {
            return _touchPositionAction.ReadValue<Vector2>();
        }

        public Vector2 getTouchWorldPosition2d()
        {
            Vector3 screenCoords = getTouchScreenPosition();
            screenCoords.z = 5;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenCoords);
            worldPos.z = 5;
            return (Vector2)worldPos;
        }
    }
}

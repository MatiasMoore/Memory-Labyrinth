using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MemoryLabyrinth.Controls
{
    [RequireComponent(typeof(PlayerInput))]
    public class TouchControls : MonoBehaviour
    {
        //Class with the control map
        private PlayerInput _playerInput;

        //Main actions
        private InputAction _touchPositionAction;
        private InputAction _touchPressAction;

        public event UnityAction touchDown;
        public event UnityAction touchUp;

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

        private void FireTouchDownEvent(InputAction.CallbackContext context)
        {
            touchDown?.Invoke();
        }

        private void FireTouchUpEvent(InputAction.CallbackContext context)
        {
            touchUp?.Invoke();
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

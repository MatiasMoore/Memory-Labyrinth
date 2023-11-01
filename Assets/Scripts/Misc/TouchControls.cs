using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class TouchControls : MonoBehaviour
{
    //Class with the control map
    private PlayerInput _playerInput;

    //Main actions
    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;

    //Lists of callback functions
    private List<System.Action<InputAction.CallbackContext>> _touchDownCallbacks = new List<System.Action<InputAction.CallbackContext>>();
    private List<System.Action<InputAction.CallbackContext>> _touchUpCallbacks = new List<System.Action<InputAction.CallbackContext>>();

    public static TouchControls Instance { get; private set; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _touchPressAction = _playerInput.actions["TouchPress"];
        _touchPositionAction = _playerInput.actions["TouchPosition"];

        if (Instance != null) return;

        Instance = this;
    }

    private void OnDisable()
    {
        //Stop all callbacks
        foreach (System.Action<InputAction.CallbackContext> func in _touchDownCallbacks)
        {
            _touchPressAction.performed -= func;
        }
        foreach (System.Action<InputAction.CallbackContext> func in _touchDownCallbacks)
        {
            _touchPressAction.canceled -= func;
        }
    }

    public void addCallbackToTouchDown(System.Action<InputAction.CallbackContext> func)
    {
        _touchPressAction.performed += func;
        _touchDownCallbacks.Add(func);
    }

    public void addCallbackToTouchUp(System.Action<InputAction.CallbackContext> func)
    {
        _touchPressAction.canceled += func;
        _touchUpCallbacks.Add(func);
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

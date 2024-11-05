using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    // References
    private InputActions _inputActions;
    
    // Properties
    public Vector2 MovementInput { get; private set; }
    public Vector2 MousePosition { get; private set; }
    
    #region Unity Methods
    private void Awake() {
        _inputActions = new InputActions();
        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;
        _inputActions.Player.Look.performed += OnLookPerformed;
    }
    private void OnDestroy() {
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;
        _inputActions.Player.Look.performed -= OnLookPerformed;
    }

    private void OnEnable() {
        _inputActions.Enable();
    }

    private void OnDisable() {
        _inputActions.Disable();
    }
    #endregion

    #region Input Events
    private void OnMovePerformed(InputAction.CallbackContext context) {
        MovementInput = context.ReadValue<Vector2>().normalized;
    }
    
    private void OnMoveCanceled(InputAction.CallbackContext context) {
        MovementInput = Vector2.zero;
    }
    
    private void OnLookPerformed(InputAction.CallbackContext context) {
        MousePosition = context.ReadValue<Vector2>();
    }
    #endregion
}
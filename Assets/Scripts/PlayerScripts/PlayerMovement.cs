using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(GameInput))]
    public class PlayerMovement : MonoBehaviour {
        // Components
        private GameInput _gameInput;
        private Rigidbody2D _rigidbody;
        private Camera _mainCamera;
        
        [Header("Movement")]
        [SerializeField]
        private float speed = 3f;
        private Vector2 _moveDirection;
        private Vector2 _mouseWorldPosition;

        #region Unity Methods
        private void Awake() {
            _gameInput = GetComponent<GameInput>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _mainCamera = Camera.main;
        }

        private void Update() {
            UpdateMovementDirection();
            FlipX(_moveDirection.x);
            UpdateMouseWorldPosition();
            LookAtMouse();
        }

        private void FixedUpdate() {
            Move();
        }
        #endregion

        #region Private Methods
        private void LookAtMouse() {
            Vector2 direction = (_mouseWorldPosition - (Vector2)transform.position).normalized;
            FlipX(direction.x);
        }

        private void UpdateMovementDirection() {
            _moveDirection = _gameInput.MovementInput;
        }
        
        private void UpdateMouseWorldPosition() {
            _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(_gameInput.MousePosition);
        }
        
        private void Move() {
            _rigidbody.linearVelocity = _moveDirection * speed;
        }
        
        private void FlipX(float x) {
            if (x != 0) {
                // Math.Sign devuelve 1 si es positivo y -1 si es negativo
                transform.localScale = new Vector3(Math.Sign(x), 1, 1);
            }
        }
        #endregion
        
        #region Public Methods
        public bool IsMoving() {
            return _rigidbody.linearVelocity.magnitude > 0;
        }
        #endregion
    }
}
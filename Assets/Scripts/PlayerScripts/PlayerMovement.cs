using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(GameInput))]
    public class PlayerMovement : MonoBehaviour {
        private GameInput _gameInput;
        private Rigidbody2D _rigidbody;
        
        [Header("Movement")]
        [SerializeField]
        private float speed = 3f;
        private Vector2 _moveDirection;

        private void Awake() {
            _gameInput = GetComponent<GameInput>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            _moveDirection = _gameInput.MovementInput;
            FlipX(_moveDirection.x);
        }

        private void FixedUpdate() {
            _rigidbody.linearVelocity = _moveDirection * speed;
        }

        private void FlipX(float x) {
            if (x != 0) {
                // Math.Sign devuelve 1 si es positivo y -1 si es negativo
                transform.localScale = new Vector3(Math.Sign(x), 1, 1);
            }
        }

        public bool IsMoving() {
            return _rigidbody.linearVelocity.magnitude > 0;
        }
    }
}
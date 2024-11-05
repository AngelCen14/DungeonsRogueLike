using System;
using UnityEngine;

namespace Characters {
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class CharacterMovement : MonoBehaviour {
        // Components
        private Rigidbody2D _rigidbody;
        
        [Header("Movement")]
        [SerializeField]
        private float speed = 3f;
        protected Vector2 MoveDirection;
        protected Vector2 PointerPosition;

        #region Unity Methods
        protected virtual void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            UpdateMovementDirection();
            FlipX(MoveDirection.x);
            UpdatePointerPosition();
            LookAtPointer();
        }

        private void FixedUpdate() {
            Move();
        }
        #endregion

        #region Private Methods
        private void LookAtPointer() {
            Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
            FlipX(direction.x);
        }
        
        private void Move() {
            _rigidbody.linearVelocity = MoveDirection * speed;
        }
        
        private void FlipX(float x) {
            if (x != 0) {
                // Math.Sign devuelve 1 si es positivo y -1 si es negativo
                transform.localScale = new Vector3(Math.Sign(x), 1, 1);
            }
        }
        #endregion

        #region Abstract Methods
        protected abstract void UpdateMovementDirection();
        protected abstract void UpdatePointerPosition();
        #endregion
        
        #region Public Methods
        public bool IsMoving() {
            return _rigidbody.linearVelocity.magnitude > 0;
        }
        #endregion
    }
}

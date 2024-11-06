using System;
using UnityEngine;

namespace Characters {
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour {
        // Components
        private Rigidbody2D _rigidbody;
        
        [Header("Movement")]
        [SerializeField]
        private float speed = 3f;
        public Vector2 MoveDirection { get; set; }
        public Vector2 PointerPosition { get; set; }

        #region Unity Methods
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            FlipX(MoveDirection.x);
            FlipToPointer();
        }

        private void FixedUpdate() {
            Move();
        }
        #endregion

        #region Private Methods
        private void FlipToPointer() {
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
        
        #region Public Methods
        public bool IsMoving() {
            return _rigidbody.linearVelocity.magnitude > 0;
        }
        #endregion
    }
}

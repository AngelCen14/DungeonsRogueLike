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

        private void FixedUpdate() {
            Move();
        }
        #endregion

        #region Private Methods
        private void Move() {
            _rigidbody.MovePosition(_rigidbody.position + MoveDirection * (speed * Time.fixedDeltaTime));
        }
        #endregion
        
        #region Public Methods
        public bool IsMoving() {
            return MoveDirection.magnitude > 0;
        }
        #endregion
    }
}

using ExtensionMethods;
using UnityEngine;
using Utils;

namespace Characters {
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour {
        // Components
        private Animator _animator;
        private CharacterMovement _characterMovement;

        #region Animator Hashes
        private readonly int _isMovingHash = Animator.StringToHash("IsMoving");
        #endregion
        
        #region Unity Methods
        private void Awake() {
            _animator = GetComponent<Animator>();
            _characterMovement = GetComponentInParent<CharacterMovement>();
        }
        
        private void Update() {
            transform.Flip(Axis.X, _characterMovement.MoveDirection.x);
            FlipToPointer();
            _animator.SetBool(_isMovingHash, _characterMovement.IsMoving());
        }
        #endregion
        
        #region Private Methods
        private void FlipToPointer() {
            Vector2 direction = (_characterMovement.PointerPosition - (Vector2)transform.position).normalized;
            transform.Flip(Axis.X, direction.x);
        }
        #endregion
    }
}
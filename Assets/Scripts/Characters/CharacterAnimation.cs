using ExtensionMethods;
using UnityEngine;
using Utils;

namespace Characters {
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour {
        // Components
        private Animator _animator;

        #region Animator Hashes
        private readonly int _isMovingHash = Animator.StringToHash("IsMoving");
        #endregion
        
        #region Unity Methods
        private void Awake() {
            _animator = GetComponent<Animator>();
        }
        #endregion
        
        #region Public Methods
        public void UpdateMoveAnimation(bool isMoving) {
            if (_animator.runtimeAnimatorController) {
                _animator.SetBool(_isMovingHash, isMoving);
            }
        }

        public void FlipSpriteToPointer(Vector2 pointerPosition) {
            Vector2 direction = (pointerPosition - (Vector2)transform.position).normalized;
            transform.Flip(Axis.X, direction.x);
        }
        #endregion
    }
}
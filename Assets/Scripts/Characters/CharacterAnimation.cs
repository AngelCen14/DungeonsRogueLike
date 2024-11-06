using UnityEngine;

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
            _animator.SetBool(_isMovingHash, _characterMovement.IsMoving());
        }
        #endregion
    }
}
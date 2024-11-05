using UnityEngine;

namespace PlayerScripts {
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimation : MonoBehaviour {
        // Components
        private Animator _animator;
        private PlayerMovement _playerMovement;

        #region Animator Hashes
        private readonly int _isMovingHash = Animator.StringToHash("IsMoving");
        #endregion
        
        #region Unity Methods
        private void Awake() {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update() {
            _animator.SetBool(_isMovingHash, _playerMovement.IsMoving());
        }
        #endregion
    }
}

using System;
using UnityEngine;

namespace PlayerScripts {
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimation : MonoBehaviour {
        private Animator _animator;
        private PlayerMovement _playerMovement;
        
        private readonly int _isMovingHash = Animator.StringToHash("IsMoving");

        private void Awake() {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update() {
            _animator.SetBool(_isMovingHash, _playerMovement.IsMoving());
        }
    }
}

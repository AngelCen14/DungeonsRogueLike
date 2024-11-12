using System;
using Characters;
using UnityEngine;
using Weapons;

namespace PlayerScripts {
    [RequireComponent(typeof(GameInput))]
    [RequireComponent(typeof(CharacterMovement))]
    public class Player : MonoBehaviour {
        // Components
        private GameInput _gameInput;
        private CharacterMovement _characterMovement;
        private Camera _mainCamera;
        private Weapon _weapon;
        
        # region Unity Methods
        protected void Awake() {
            _gameInput = GetComponent<GameInput>();
            _characterMovement = GetComponent<CharacterMovement>();
            _mainCamera = Camera.main;
            _weapon = GetComponentInChildren<Weapon>();
        }

        private void Start() {
            _gameInput.AttackEvent += OnAttack;
        }

        private void Update() {
            HandleMovement();
            _weapon.PointerPosition = GetPointerPosition();
        }

        private void OnDisable() {
            _gameInput.AttackEvent -= OnAttack;
        }

        # endregion

        #region Event Handlers
        private void OnAttack(object sender, EventArgs e) {
            _weapon.Attack();
        }
        #endregion
        
        #region Private Methods
        private Vector2 GetPointerPosition() {
            return _mainCamera.ScreenToWorldPoint(_gameInput.MousePosition);
        }

        private void HandleMovement() {
            // Pasar el input al CharacterMovement
            _characterMovement.MoveDirection = _gameInput.MovementInput;
            _characterMovement.PointerPosition = GetPointerPosition();
        }
        #endregion
    }
}
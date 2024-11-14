using System;
using UnityEngine;

namespace Characters.PlayerScripts {
    [RequireComponent(typeof(GameInput))]
    public class Player : Character {
        // Components
        private GameInput _gameInput;
        private Camera _mainCamera;
        
        #region Unity Methods
        protected override void Awake() {
            base.Awake();
            _gameInput = GetComponent<GameInput>();
            _mainCamera = Camera.main;
        }

        protected void Start() {
            _gameInput.AttackEvent += OnAttack;
        }

        private void OnDisable() {
            _gameInput.AttackEvent -= OnAttack;
        }
        #endregion
        
        #region Overrides
        protected override void UpdateMoveAndPointer() {
            MoveDirection = _gameInput.MovementInput.normalized;
            PointerPosition = _mainCamera.ScreenToWorldPoint(_gameInput.MousePosition);
        }
        #endregion

        #region Event Handlers
        private void OnAttack(object sender, EventArgs e) {
            Weapon.Attack();
        }
        #endregion
    }
}
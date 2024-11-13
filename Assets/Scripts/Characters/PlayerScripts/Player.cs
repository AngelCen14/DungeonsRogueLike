using System;
using UnityEngine;

namespace Characters.PlayerScripts {
    [RequireComponent(typeof(GameInput))]
    [RequireComponent(typeof(CharacterMovement))]
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

        protected override void Update() {
            base.Update();
            Weapon.PointerPosition = GetPointerPosition();
        }

        private void OnDisable() {
            _gameInput.AttackEvent -= OnAttack;
        }

        #endregion

        #region Event Handlers
        private void OnAttack(object sender, EventArgs e) {
            Weapon.Attack();
        }
        #endregion

        #region Overrided Methods
        protected override Vector2 GetMoveDirection() {
            return _gameInput.MovementInput.normalized;
        }
        protected override Vector2 GetPointerPosition() {
            return _mainCamera.ScreenToWorldPoint(_gameInput.MousePosition);
        }
        #endregion
    }
}
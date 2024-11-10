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
        private WeaponParent _weaponParent;
        
        # region Unity Methods
        protected void Awake() {
            _gameInput = GetComponent<GameInput>();
            _characterMovement = GetComponent<CharacterMovement>();
            _mainCamera = Camera.main;
            _weaponParent = GetComponentInChildren<WeaponParent>();
        }

        private void Update() {
            HandleMovement();
            _weaponParent.PointerPosition = GetPointerPosition();
        }
        # endregion

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
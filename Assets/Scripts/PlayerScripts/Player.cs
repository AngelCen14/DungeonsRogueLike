using Characters;
using UnityEngine;

namespace PlayerScripts {
    [RequireComponent(typeof(GameInput))]
    [RequireComponent(typeof(CharacterMovement))]
    public class Player : MonoBehaviour {
        // Components
        private GameInput _gameInput;
        private CharacterMovement _characterMovement;
        private Camera _mainCamera;
        
        # region Unity Methods
        protected void Awake() {
            _gameInput = GetComponent<GameInput>();
            _characterMovement = GetComponent<CharacterMovement>();
            _mainCamera = Camera.main;
        }

        private void Update() {
            // Pasar el input al CharacterMovement
            _characterMovement.MoveDirection = _gameInput.MovementInput;
            _characterMovement.PointerPosition = _mainCamera.ScreenToWorldPoint(_gameInput.MousePosition);
        }
        # endregion
    }
}
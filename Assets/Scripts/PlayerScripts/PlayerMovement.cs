using Characters;
using UnityEngine;

namespace PlayerScripts {
    [RequireComponent(typeof(GameInput))]
    public class PlayerMovement : CharacterMovement {
        // Components
        private GameInput _gameInput;
        private Camera _mainCamera;
        
        protected override void Awake() {
            base.Awake();
            _gameInput = GetComponent<GameInput>();
            _mainCamera = Camera.main;
        }

        protected override void UpdateMovementDirection() {
            MoveDirection = _gameInput.MovementInput;
        }

        protected override void UpdatePointerPosition() {
            PointerPosition = _mainCamera.ScreenToWorldPoint(_gameInput.MousePosition);
        }
    }
}
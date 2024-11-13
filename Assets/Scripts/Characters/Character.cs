using UnityEngine;
using Weapons;

namespace Characters {
    [RequireComponent(typeof(CharacterMovement))]
    public abstract class Character : MonoBehaviour, IDamageable {
        // Components
        protected CharacterMovement _characterMovement;
        protected CharacterAnimation _characterAnimation;
        protected Weapon _weapon;

        # region Unity Methods
        protected virtual void Awake() {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterAnimation = GetComponentInChildren<CharacterAnimation>();
            _weapon = GetComponentInChildren<Weapon>();
        }

        protected virtual void Update() {
            HandleMovement();
            HandleAnimation();
        }
        # endregion

        #region Private Methods
        private void HandleMovement() {
            _characterMovement.MoveDirection = GetMoveDirection();
            _characterMovement.PointerPosition = GetPointerPosition();
        }

        private void HandleAnimation() {
            _characterAnimation.UpdateMoveAnimation(_characterMovement.IsMoving());
            _characterAnimation.FlipSpriteToPointer(_characterMovement.PointerPosition);
        }
        #endregion

        #region Abstract Methods
        protected abstract Vector2 GetMoveDirection();
        protected abstract Vector2 GetPointerPosition();
        #endregion

        #region Interfaces Implementations
        public void Damage(float damage) {
            Debug.Log($"{gameObject.name} received {damage} of damage");
        }
        #endregion
    }
}


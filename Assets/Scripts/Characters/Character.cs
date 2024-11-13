using Interfaces;
using UnityEngine;
using Weapons;

namespace Characters {
    [SelectionBase]
    [RequireComponent(typeof(CharacterMovement))]
    public abstract class Character : MonoBehaviour, IDamageable {
        // Components
        private CharacterMovement _characterMovement;
        private CharacterAnimation _characterAnimation;
        protected Weapon Weapon;

        #region Unity Methods
        protected virtual void Awake() {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterAnimation = GetComponentInChildren<CharacterAnimation>();
            Weapon = GetComponentInChildren<Weapon>();
        }

        protected virtual void Update() {
            HandleMovement();
            HandleAnimation();
        }
        #endregion

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


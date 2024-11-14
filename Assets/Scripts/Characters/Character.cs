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
        
        protected Vector2 MoveDirection { get; set; }
        protected Vector2 PointerPosition { get; set; }
        
        private int _health = 3;

        #region Unity Methods
        protected virtual void Awake() {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterAnimation = GetComponentInChildren<CharacterAnimation>();
            Weapon = GetComponentInChildren<Weapon>();
        }

        protected virtual void Update() {
            UpdateMoveAndPointer();
            HandleMovement();
            HandleAnimation();
            RotateWeapon();
        }
        #endregion

        #region Private Methods
        private void HandleMovement() {
            _characterMovement.MoveDirection = MoveDirection;
            _characterMovement.PointerPosition = PointerPosition;
        }

        private void HandleAnimation() {
            _characterAnimation.UpdateMoveAnimation(_characterMovement.IsMoving());
            _characterAnimation.FlipSpriteToPointer(_characterMovement.PointerPosition);
        }

        private void RotateWeapon() {
            Weapon.PointerPosition = PointerPosition;
        }
        #endregion
        
        #region Abstract Methods
        protected abstract void UpdateMoveAndPointer();
        #endregion
        
        #region Interfaces Implementations
        public void Damage(float damage) {
            _health -= (int)damage;
            Debug.Log($"{gameObject.name} received {damage} of damage");
            if (_health <= 0) {
                Debug.Log($"{gameObject.name} should die!");
            }
        }
        #endregion
    }
}


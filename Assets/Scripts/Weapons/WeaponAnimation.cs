using UnityEngine;

namespace Weapons {
    [RequireComponent(typeof(Animator))]
    public class WeaponAnimation : MonoBehaviour {
        // Components
        private Animator _animator;
        private Weapon _parentWeapon;

        #region Animator Hashes
        private readonly int _attackTrigger = Animator.StringToHash("AttackTrigger");
        #endregion
        
        #region Unity Methods
        private void Awake() {
            _animator = GetComponent<Animator>();
            _parentWeapon = GetComponentInParent<Weapon>();
        }
        #endregion
        
        #region Public Methods
        public void TriggerAttackAnimation() {
            _animator.SetTrigger(_attackTrigger);
        }
        
        // Se llama desde un evento de la animacion
        public void ResetAttackingState() {
            _parentWeapon.IsAttacking = false;
        }
        #endregion
    }
}
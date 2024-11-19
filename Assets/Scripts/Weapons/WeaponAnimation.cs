using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Weapons {
    [RequireComponent(typeof(Animator))]
    public class WeaponAnimation : MonoBehaviour {
        // Components
        private Animator _animator;

        #region Animator Hashes
        private readonly int _attackTrigger = Animator.StringToHash("AttackTrigger");
        #endregion
        
        #region Events
        public event Action AttackAnimationFinished;
        #endregion
        
        #region Unity Methods
        private void Awake() {
            _animator = GetComponent<Animator>();
        }
        #endregion
        
        #region Public Methods
        public void TriggerAttackAnimation() {
            _animator.SetTrigger(_attackTrigger);
        }
        #endregion
        
        #region Animation Events
        // Se llama desde el evento al final de la animacion de ataque
        [Preserve]
        public void OnAttackAnimationFinished() {
            AttackAnimationFinished?.Invoke();
        }
        #endregion
        
    }
}
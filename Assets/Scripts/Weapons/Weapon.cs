using System.Collections;
using ExtensionMethods;
using Interfaces;
using UnityEngine;
using Utils;

namespace Weapons {
    public class Weapon : MonoBehaviour {
        public Vector2 PointerPosition { get; set; }
        private Vector2 _direction;
        private float _rotationAngle;
        
        private WeaponAnimation _weaponAnimation;
        
        [Header("Rendering")]
        [SerializeField] 
        private SpriteRenderer characterRenderer;
        private SpriteRenderer _weaponRederer;
        
        [Header("Attack")]
        [SerializeField]
        private float delay = 0.3f;
        
        [SerializeField]
        private Transform attackOrigin;

        [SerializeField]
        private float attackRadius;

        [SerializeField]
        private LayerMask attackLayer;

        private bool _canAttack;
        private bool _isAttacking;
        
        #region Unity Methods
        private void Awake() {
            _weaponRederer = GetComponentInChildren<SpriteRenderer>();
            _weaponAnimation = GetComponentInChildren<WeaponAnimation>();
        }

        private void Start() {
            _weaponAnimation.AttackAnimationFinished += OnAttackAnimationFinished;
            _canAttack = true;
            _isAttacking = false;
        }
        
        private void Update() {
            // Hasta que no acabe el ataque no se podra mover el arma
            if (_isAttacking) return; 
            
            _direction = (PointerPosition - (Vector2)transform.position).normalized;
            
            // Calcular la rotacion, bloqueando x e y a 0, ya que solo debe rotar en z
            _rotationAngle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, _rotationAngle);
            
            transform.Flip(Axis.Y, _direction.x);
            SetSpriteSortingOrder();
        }

        private void OnDisable() {
            _weaponAnimation.AttackAnimationFinished -= OnAttackAnimationFinished;
        }

        private void OnDrawGizmos() {
            #region Weapon Angle
            if (PointerPosition != Vector2.zero) {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, (Vector2)transform.position + _direction);
                Gizmos.color = Color.blue;
                Vector2 reference = (Vector2)transform.position + Vector2.right;
                Gizmos.DrawLine(transform.position, reference);
#if UNITY_EDITOR
                UnityEditor.Handles.Label(transform.position + Vector3.up * 0.7f + Vector3.right * 0.3f,
                    $"{_rotationAngle:F2}°");
#endif
            }
            #endregion

            #region Attack Radius
            if (attackOrigin) {
                Gizmos.color = Color.blue;
                if (_isAttacking) Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
            }
            #endregion
        }
        #endregion

        #region Event Handlers
        private void OnAttackAnimationFinished() {
            _isAttacking = false;
        }
        #endregion
        
        #region Private Methods
        // Coloca el arma por delante o por detras del jugador en base a la rotacion del arma
        private void SetSpriteSortingOrder() {
            if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180) {
                _weaponRederer.sortingOrder = characterRenderer.sortingOrder - 1;
            } else {
                _weaponRederer.sortingOrder = characterRenderer.sortingOrder + 1;
            }
        }
        #endregion
        
        #region Public Methods
        public void Attack() {
            if (!_canAttack) return;
            _weaponAnimation.TriggerAttackAnimation();
            _isAttacking = true;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius,attackLayer);
            foreach (Collider2D c in colliders) {
                if (c.TryGetComponent<IDamageable>(out IDamageable damageable)) {
                    damageable.Damage(1);
                }
            }

            StartCoroutine(AttackDelay());
        }
        #endregion

        #region Coroutines
        private IEnumerator AttackDelay() {
            _canAttack = false;
            yield return new WaitForSeconds(delay);
            _canAttack = true;
        }
        #endregion
    }
}
using System.Collections;
using ExtensionMethods;
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
            if (PointerPosition != Vector2.zero) {
                // Configura el color del Gizmo
                Gizmos.color = Color.red;
        
                // Dibuja una línea desde el objeto hacia el puntero, mostrando la dirección
                Gizmos.DrawLine(transform.position, (Vector2)transform.position + _direction);

                // Cambia el color para el eje de referencia
                Gizmos.color = Color.blue;

                // Dibuja una línea que representa el eje X del objeto (sin rotación)
                Vector2 reference = (Vector2)transform.position + Vector2.right;
                Gizmos.DrawLine(transform.position, reference);

                // Muestra el valor del ángulo en la escena (solo en modo Play)
#if UNITY_EDITOR
                UnityEditor.Handles.Label(transform.position + Vector3.up * 0.5f + Vector3.right * 0.2f, 
                    $"{_rotationAngle:F2}°");
#endif
            }
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
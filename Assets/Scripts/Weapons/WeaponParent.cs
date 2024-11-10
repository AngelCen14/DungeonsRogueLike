using ExtensionMethods;
using UnityEngine;
using Utils;

namespace Weapons {
    public class WeaponParent : MonoBehaviour {
        public Vector2 PointerPosition { get; set; }
        private Vector2 _direction;
        private float _rotationAngle;

        [SerializeField] 
        private SpriteRenderer characterRenderer;
        private SpriteRenderer _weaponRederer;

        #region Unity Methods
        private void Awake() {
            _weaponRederer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update() {
            _direction = (PointerPosition - (Vector2)transform.position).normalized;
            
            // Calcular la rotacion, bloqueando x e y a 0, ya que solo debe rotar en z
            _rotationAngle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, _rotationAngle);
            
            transform.Flip(Axis.Y, _direction.x);
            SetSpriteSortingOrder();
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
    }
}
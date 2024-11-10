using ExtensionMethods;
using UnityEngine;
using Utils;

namespace Weapons {
    public class WeaponParent : MonoBehaviour {
        public Vector2 PointerPosition { get; set; }

        private void Update() {
            Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
            transform.right = direction;
            transform.Flip(Axis.Y, direction.x);
        }
    }
}
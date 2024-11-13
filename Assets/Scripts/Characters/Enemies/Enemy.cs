using UnityEngine;

namespace Characters.Enemies {
    public class Enemy : Character {
        protected override Vector2 GetMoveDirection() {
            return Vector2.zero;
        }

        protected override Vector2 GetPointerPosition() {
            return Vector2.zero;
        }
    }
}
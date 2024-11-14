using Characters.PlayerScripts;
using UnityEngine;

namespace Characters.Enemies {
    public class Enemy : Character {
        
        [SerializeField]
        private Player player;
        
        protected override Vector2 GetMoveDirection() {
            return Vector2.zero;
        }

        protected override Vector2 GetPointerPosition() {
            return player.transform.position;
        }
    }
}
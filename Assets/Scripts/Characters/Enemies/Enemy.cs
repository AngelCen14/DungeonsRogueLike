using Characters.PlayerScripts;
using UnityEngine;

namespace Characters.Enemies {
    public class Enemy : Character {
        
        [SerializeField]
        private Player player;

        [SerializeField] 
        private float chaseDistanceThreshold = 3f;
        
        [SerializeField] 
        private float attackDistanceThreshold = 0.8f;
        
        #region Overrides
        protected override void UpdateMoveAndPointer() {
            if (!player) return;
            
            float distance = Vector2.Distance(player.transform.position, transform.position);
            if (distance < chaseDistanceThreshold) {
                PointerPosition = player.transform.position;
                if (distance <= attackDistanceThreshold) {
                    MoveDirection = Vector2.zero;
                    Weapon.Attack();
                } else {
                    MoveDirection = (player.transform.position - transform.position).normalized;
                }
            }
        }
        #endregion
    }
}
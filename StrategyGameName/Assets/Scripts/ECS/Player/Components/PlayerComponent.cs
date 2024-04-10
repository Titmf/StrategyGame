using UnityEngine;

namespace ECS.Player.Components {
    struct PlayerComponent {
        
        public Transform PlayerTransform;
        public Rigidbody PlayerRb;
        public CapsuleCollider PlayerCollider;
        public float PlayerSpeed;
        //public int Mana;
    }
}
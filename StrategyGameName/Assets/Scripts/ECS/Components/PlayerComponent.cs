using UnityEngine;

namespace ECS.Components {
    struct PlayerComponent {
        public Transform PlayerTransform;
        public Rigidbody PlayerRb;
        public CapsuleCollider PlayerCollider;
        public Vector3 PlayerVelocity;
        public float PlayerSpeed;
        public int Mana;
    }
}
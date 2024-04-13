using UnityEngine;

namespace ECS.Player.Components {
    struct PlayerComponent {
        public Transform PlayerTransform;
        public Rigidbody PlayerRb;
        //public SphereCollider PlayerCollider;
        public float PlayerSpeed;
    }
}
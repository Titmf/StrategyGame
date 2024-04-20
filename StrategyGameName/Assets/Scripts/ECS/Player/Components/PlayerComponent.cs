using UnityEngine;

namespace ECS.Player.Components {
    struct PlayerComponent {
        public Transform PlayerTransform;
        public Rigidbody PlayerRb;
        public HexCoordinates PlayerPositionByHexCoordinates;
        public int PlayerDirectionIndex;
    }
}
using UnityEngine;

namespace ECS.Components
{
    public struct CameraComponent
    {
        public Transform CameraTransform;
        public Vector3 Offset;
        public float CameraSmoothness;
    }
}
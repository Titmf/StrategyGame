using UnityEngine;

namespace ECS.Data
{
    public static class Constants
    {
        public static class Tags
        {
            public const string ManaTag = "Mana";
        }
        public struct PlayerDefaultCharacteristics
        {
            public const float PlayerDefaultStepDistance = HexMetrics.innerRadius;
            public const float RotationStepAngle = 60f;
            
            public const int PlayerMaxHealth = 100;
            public const int PlayerManaMax = 100;
        }
        public struct InputConfigs
        {
            public const float PlayerDefaultRotationSpeed = 100f;
            
            public const float RotationStepThreshold = 0.5f;
            public const float RotationStepAccelerationRate = 1f;
            
            public const float StepThreshold = 0.5f;
            public const float StepAccelerationRate = 1f;
        }
        public static class CameraDefaultConfiguration
        {
            public const float CameraFollowSmoothness = 1f;
            public static readonly Vector3 CameraFollowOffset = new Vector3(0f, 100f, -100f);
        }
    }
}
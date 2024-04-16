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
            public const float PlayerDefaultStepDistance = HexMetrics.innerRadius * 2f;
            public const float RotationStepAngle = 60f;
            
            public const float PlayerDefaultStepDuration = 0.4f;
            public const float PlayerDefaultRotationDuration  = 0.2f;
            
            public const int PlayerMaxHealth = 100;
            public const int PlayerManaMax = 100;
        }
        public struct InputRotation
        {
            public const float Left = -1f;
            public const float Right = 1f;
        }
        public static class CameraDefaultConfiguration
        {
            public const float CameraFollowSmoothness = 1f;
            public static readonly Vector3 CameraFollowOffset = new Vector3(0f, 100f, -100f);
        }
        public static class PlayerDefaultConfiguration
        {
            public static readonly Quaternion PlayerRotationOffsetAtInit = Quaternion.Euler(0f, 90f, 0f);
        }
    }
}
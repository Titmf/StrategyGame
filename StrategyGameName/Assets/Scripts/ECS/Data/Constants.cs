using UnityEngine;

namespace ECS.Data
{
    public static class Constants
    {
        public static class Tags
        {
            public const string ManaTag = "Mana";
        }
        public static class PlayerDefaultCharacteristics
        {
            public const float PlayerDefaultSpeed = HexMetrics.innerRadius;
            public const float PlayerDefaultRotationSpeed = 5f;
            
            public const int PlayerMaxHealth = 100;
            public const int PlayerManaMax = 100;
        }
        
        public static class InputIntervals
        {
            public const float MoveInterval = 0.1f;
            public const float RotateInterval = 0.2f;
        }
        public static class CameraDefaultConfiguration
        {
            public const float CameraFollowSmoothness = 1f;
            public static readonly Vector3 CameraFollowOffset = new Vector3(0f, 100f, -100f);
        }
    }
}
using UnityEngine;

namespace ECS.Data
{
    public static class Constants
    {
        public static class Tags
        {
            public const string ManaTag = "Mana";
        }
        public static class Numbers
        {
            public const float PlayerDefaultSpeed = 5f;
            public const float CameraFollowSmoothness = 1f;
            public const int PlayerMaxHealth = 100;
        }
        public static class Vectors
        {
            public static readonly Vector3 CameraFollowOffset = new Vector3(0f, 100f, -100f);
        }
    }
}
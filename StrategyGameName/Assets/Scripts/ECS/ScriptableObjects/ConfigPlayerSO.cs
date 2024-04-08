using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "Configuration")]
    public class ConfigPlayerSo : ScriptableObject
    {
        public float playerSpeed;
        public float cameraFollowSmoothness;
    }
}
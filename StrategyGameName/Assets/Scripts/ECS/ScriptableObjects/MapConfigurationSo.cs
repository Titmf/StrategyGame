using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu(fileName = "MapConfigurationSo", menuName = "ECS/Data/MapConfigurationSo")]
    public class MapConfigurationSo : ScriptableObject
    {
        public int ChunkCountX;
        public int ChunkCountZ;
    }
}
using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu(fileName = "GameSceneConfigurationSo", menuName = "ECS/Data/GameSceneConfigurationSo")]
    public class GameSceneConfigurationSo : ScriptableObject
    {
        public int ChunkCountX;
        public int ChunkCountZ;
    }
}
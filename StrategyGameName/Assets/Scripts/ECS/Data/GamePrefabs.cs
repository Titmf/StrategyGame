using ECS.Services.SceneManagement;

using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu(fileName = "GamePrefabs", menuName = "ECS/Data/GamePrefabs")]
    public class GamePrefabs : ScriptableObject
    {
        public GameObject PlayerPrefab;
        public SceneLoader SceneServiceLoader;
    }
}
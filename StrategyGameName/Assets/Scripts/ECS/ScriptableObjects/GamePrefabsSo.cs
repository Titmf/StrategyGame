using ECS.Services.SceneManagement;

using UnityEngine;

namespace ECS.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GamePrefabs", menuName = "ECS/Data/GamePrefabs")]
    public class GamePrefabsSo : ScriptableObject
    {
        public GameObject PlayerPrefab;
        public SceneLoader SceneServiceLoader;
    }
}
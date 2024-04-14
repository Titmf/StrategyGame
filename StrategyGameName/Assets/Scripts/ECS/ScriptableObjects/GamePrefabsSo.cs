using Cinemachine;

using ECS.Services.SceneManagement;

using Hex;

using UnityEngine;

namespace ECS.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GamePrefabs", menuName = "ECS/Data/GamePrefabs")]
    public class GamePrefabsSo : ScriptableObject
    {
        public GameObject PlayerPrefab;
        public CinemachineBrain PlayerCameraPrefab;
        public SceneLoader SceneServiceLoader;
        public HexGrid HexGridPrefab;
    }
}
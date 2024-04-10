
using TMPro;

using UnityEngine;

namespace ECS.Data
{
    public class GameSceneData
    {
        public GamePrefabs GamePrefabs;
        public Transform PlayerSpawnPoint;

        public TextMeshProUGUI CoinCounter;
        public GameObject PlayerWonPanel;
        public GameObject GameOverPanel;
    }
}
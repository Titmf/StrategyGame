
using ECS.ScriptableObjects;

using TMPro;

using UnityEngine;

namespace ECS.Data
{
    public class GameSceneData
    {
        public GamePrefabsSo GamePrefabsSo;
        public Transform PlayerSpawnPoint;

        public TextMeshProUGUI CoinCounter;
        public GameObject PlayerWonPanel;
        public GameObject GameOverPanel;
    }
}
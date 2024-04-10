using ECS.Data;
using ECS.Player;
using ECS.Services.SceneManagement;
using ECS.Systems;

using Leopotam.EcsLite;

using TMPro;

using UnityEngine;

namespace ECS.Start {
    sealed class EcsStartup : MonoBehaviour {
        
        private EcsWorld _ecsWorld;
        private EcsSystems _ecsInitSystem;
        private EcsSystems _updateSystem;
        private EcsSystems _fixedUpdateSystem;

        [SerializeField] private GamePrefabs _gamePrefabs;
        [SerializeField] private TextMeshProUGUI _coinCounter;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _playerWonPanel;
        [SerializeField] private Transform _playerSpawnPoint;

        private void Start ()
        {
            _ecsWorld = new EcsWorld();
            var gameSceneData = new GameSceneData();
            
            gameSceneData.GamePrefabs = _gamePrefabs;
            gameSceneData.CoinCounter = _coinCounter;
            gameSceneData.GameOverPanel = _gameOverPanel;
            gameSceneData.PlayerWonPanel = _playerWonPanel;
            gameSceneData.PlayerSpawnPoint = _playerSpawnPoint;
            
            // TODO to monobeh? 
            
            _ecsInitSystem = new EcsSystems (_ecsWorld, gameSceneData)
                .Add (new PlayerInitSystem())
                
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
                
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                ;
            
            _ecsInitSystem.Init();

            _updateSystem = new EcsSystems(_ecsWorld, gameSceneData)
                .Add(new PlayerInputSystem());
            
            _updateSystem.Init();
            
            _fixedUpdateSystem = new EcsSystems(_ecsWorld, gameSceneData)
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem());
            
            _fixedUpdateSystem.Init();
        }

        private void Update () {
            _updateSystem.Run();
        }
        
        private void FixedUpdate () {
            _fixedUpdateSystem.Run ();
        }

        private void OnDestroy ()
        {
            if (_ecsInitSystem == null) return;

            _ecsInitSystem.Destroy ();

            _updateSystem.Destroy ();
            
            _fixedUpdateSystem.Destroy ();
            
            // add here cleanup for custom worlds, for example:
            // _systems.GetWorld ("events").Destroy ();
            
            _ecsInitSystem.GetWorld ().Destroy ();
            
            _fixedUpdateSystem = null;
            _updateSystem = null;
            _ecsInitSystem = null;
        }
    }
}
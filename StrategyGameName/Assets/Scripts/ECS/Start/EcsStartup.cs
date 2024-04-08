using ECS.Data;
using ECS.Services.SceneManagement;
using ECS.Systems;
using ECS.Systems.Player;

using Leopotam.EcsLite;

using Platformer;

using SceneManagement;

using UnityEngine;
using UnityEngine.UI;

namespace ECS.Start {
    sealed class EcsStartup : MonoBehaviour {
        private EcsWorld _ecsWorld;    
        private EcsSystems _ecsInitSystem;
        private EcsSystems _updateSystem;
        private EcsSystems _fixedUpdateSystem;

        [SerializeField] private ConfigPlayerSo _configurationSo;
        [SerializeField] private Text _coinCounter;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _playerWonPanel;
        [SerializeField] private SceneLoader _sceneServiceLoader;
        private void Start ()
        {
            _ecsWorld = new EcsWorld();
            var gameSceneData = new GameSceneData();
            
            gameSceneData.configuration = _configurationSo;
            gameSceneData.coinCounter = _coinCounter;
            gameSceneData.gameOverPanel = _gameOverPanel;
            gameSceneData.playerWonPanel = _playerWonPanel;
            gameSceneData.SceneServiceLoader = _sceneServiceLoader;
            
            // register your shared data here, for example:
            // var shared = new Shared ();
            // systems = new EcsSystems (new EcsWorld (), shared);
            
            _ecsInitSystem = new EcsSystems (_ecsWorld, gameSceneData)
                .Add (new PlayerInitSystem())
                
                // register your systems here, for example:
                // .Add (new TestSystem1 ())
                // .Add (new TestSystem2 ())
                
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
            _ecsInitSystem?.Run ();
        }
        
        private void FixedUpdate () {
            _fixedUpdateSystem?.Run ();
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